using CardsAgainstHumanity.DatabaseAccess.DataAccess;
using Microsoft.EntityFrameworkCore;

namespace CardsAgainstHumanity.API;

public class Program {
    public static void Main(string[] args) {
        var builder = WebApplication.CreateBuilder(args);

        builder.Services.AddControllers();

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

        app.UseAuthorization();


        app.MapControllers();

        app.Run();
    }
}
