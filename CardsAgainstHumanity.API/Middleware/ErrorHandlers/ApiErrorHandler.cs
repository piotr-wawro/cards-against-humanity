namespace CardsAgainstHumanity.API.Middleware.ErrorHandlers;

public class ApiErrorHandler : IMiddleware {
    public async Task InvokeAsync(HttpContext context, RequestDelegate next) {
        try {
            await next(context);
        }
        catch (ApiError e) {
            context.Response.StatusCode = e.Code;
            context.Response.ContentType = "application/json";
            await context.Response.WriteAsJsonAsync(new { error = e.Message });
        }
    }
}
