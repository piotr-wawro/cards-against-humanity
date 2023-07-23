namespace CardsAgainstHumanity.API.Middleware.ErrorHandlers;

public class GeneralErrorHandler : IMiddleware {
    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        try {
            await next(context);
        }
        catch (Exception) {
            context.Response.StatusCode = 500;
            context.Response.ContentType = "application/json";
            await context.Response.WriteAsJsonAsync(new { error = "Something went wrong." });
        }
    }
}
