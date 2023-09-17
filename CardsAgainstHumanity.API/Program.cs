using CardsAgainstHumanity.API.Middleware.ErrorHandlers;
using CardsAgainstHumanity.API.Services;
using CardsAgainstHumanity.DatabaseAccess.DataAccess;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Security.Claims;
using System.Text;

namespace CardsAgainstHumanity.API;

public class Program {
    public static void Main(string[] args) {
        var builder = WebApplication.CreateBuilder(args);

        builder.Services.AddControllers();

        builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(config => {
                config.TokenValidationParameters = new() {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = builder.Configuration["JWT:ValidIssuer"],
                    ValidAudience = builder.Configuration["JWT:ValidAudience"],
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JWT:Secret"]))
                };
            });
        builder.Services.AddAuthorization(config => {
            config.AddPolicy("Admin", policy => policy.RequireClaim(ClaimTypes.Role, "Admin"));
            config.AddPolicy("User", policy => policy.RequireClaim(ClaimTypes.Role, "User", "Admin"));
        });

        builder.Services.AddScoped<ICardService, CardService>();
        builder.Services.AddScoped<IDeckService, DeckService>();
        builder.Services.AddScoped<IAuthenticationService, AuthenticationService>();
        builder.Services.AddScoped<GeneralErrorHandler>();
        builder.Services.AddScoped<ApiErrorHandler>();

        builder.Services.AddSwaggerGen(c => {
            c.SwaggerDoc("v1", new() { Title = "CardsAgainstHumanity.API", Version = "v1" });
            c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme {
                In = ParameterLocation.Header,
                Name = "Authorization",
                Type = SecuritySchemeType.Http,
                BearerFormat = "JWT",
                Scheme = "Bearer"
            });
            c.AddSecurityRequirement(new OpenApiSecurityRequirement {
                {
                    new OpenApiSecurityScheme {
                        Reference = new OpenApiReference {
                            Type=ReferenceType.SecurityScheme,
                            Id="Bearer"
                        }
                    },
                    new string[] {}
                }
            });
        });

        builder.Services.AddDbContext<CahDbContext>(options => {
            options.UseSqlServer(
                builder.Configuration.GetConnectionString("CahDb"),
                x => _ = builder.Environment.IsDevelopment() switch {
                    true => x.MigrationsAssembly("CardsAgainstHumanity.DevelopmentMigrations"),
                    false => x.MigrationsAssembly("CardsAgainstHumanity.ReleaseMigrations"),
                }
            );
        });

        var app = builder.Build();

        using(var scope = app.Services.CreateScope()) {
            using(var context = scope.ServiceProvider.GetRequiredService<CahDbContext>()) {
                if(app.Environment.IsProduction()) return;
                else context.Database.Migrate();
            }
        }

        app.UseHttpsRedirection();

        app.UseMiddleware<GeneralErrorHandler>();
        app.UseMiddleware<ApiErrorHandler>();

        app.UseAuthentication();
        app.UseAuthorization();

        app.UseSwagger();
        app.UseSwaggerUI(c => {
            c.SwaggerEndpoint(
                "/swagger/v1/swagger.json",
                "CardsAgainstHumanity.API v1"
            );
        });

        app.MapControllers();

        app.Run();
    }
}
