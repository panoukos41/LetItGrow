namespace LetItGrow.Microservice.Common.Extensions
{
    public static class HandlerExtensions
    {
        /// <summary>
        /// Check if the user id is null, if it is then throw a <see cref="ErrorException"/>
        /// of <see cref="Errors.Unauthorized"/>.
        /// </summary>
        /// <param name="userId">The id to validate.</param>
        /// <returns>The user id if it is not null.</returns>
        public static string CheckUserId(this string? userId)
        {
            if (userId is null)
            {
                throw new ErrorException(Errors.Unauthorized);
            }
            return userId;
        }
    }
}