namespace LetItGrow.Microservice.Common
{
    /// <summary>
    /// Common list of errors.
    /// </summary>
    public static class Errors
    {
        public static readonly Error Continue = new(
            title: "Continue",
            detail: "Continue, or ignore the response if the request is already finished.",
            status: 100);

        public static readonly Error Validation = new(
            title: "Validation Error",
            detail: "A validation error for the data has occured.",
            status: 400);

        public static readonly Error Unauthorized = new(
            title: "Unauthorized",
            detail: "The user is not authorized to access this resource.",
            status: 401);

        public static readonly Error NotFound = new(
            title: "Not Found",
            detail: "You have provided a key that doesn't exist.",
            status: 404);

        public static readonly Error Conflict = new(
            title: "Conflict",
            detail: "The entity has been modified or deleted since you last requested it. You should reload it and try again.",
            status: 409);

        public static readonly Error InternalServerError = new(
            title: "Internal Server Error",
            detail: "An error occured in the server",
            status: 500);

        public static readonly Error ServiceUnavailable = new(
            title: "Service Unavailable",
            detail: "The service is not currently available",
            status: 503);
    }
}