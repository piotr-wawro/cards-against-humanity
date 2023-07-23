namespace CardsAgainstHumanity.API.Middleware.ErrorHandlers;

public class ApiError : Exception {
    public int Code { get; private set; }

    private ApiError(string message, int code) : base(message) {
        Code = code;
    }

    public static ApiError BadRequest(string message) {
        return new ApiError(message, 400);
    }

    public static ApiError Unauthorized(string message) {
        return new ApiError(message, 401);
    }

    public static ApiError Forbidden(string message) {
        return new ApiError(message, 403);
    }

    public static ApiError NotFound(string message) {
        return new ApiError(message, 404);
    }

    public static ApiError Conflict(string message) {
        return new ApiError(message, 409);
    }
}
