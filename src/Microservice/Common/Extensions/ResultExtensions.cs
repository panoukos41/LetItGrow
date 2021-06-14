using System;
using System.Threading.Tasks;

namespace LetItGrow.Microservice.Common
{
    public static class ResultExtensions
    {
        /// <summary>
        /// Asynchronously executes the <see cref="Task"/> then executes the match method.
        /// </summary>
        /// <typeparam name="T">The type of the result.</typeparam>
        /// <param name="task">The task to execute.</param>
        /// <param name="result">The result action.</param>
        /// <param name="error">The error action.</param>
        public static async Task SwitchAsync<T>(this Task<Result<T>> task, Action<T> result, Action<Error> error)
        {
            (await task).Switch(result, error);
        }

        /// <summary>
        /// Asynchronously executes the <see cref="Task"/> then executes the match method and returns the result.
        /// </summary>
        /// <typeparam name="T">The type of the result.</typeparam>
        /// <typeparam name="TResult">The type of the match result.</typeparam>
        /// <param name="task">The task to execute.</param>
        /// <param name="result">The result action.</param>
        /// <param name="error">The error action.</param>
        /// <returns>Returns the result of the match method.</returns>
        public static async Task<TResult> MatchAsync<T, TResult>(this Task<Result<T>> task, Func<T, TResult> result, Func<Error, TResult> error)
        {
            return (await task).Match(result, error);
        }

        /// <summary>
        /// Asynchronously executes the <see cref="ValueTask"/> then executes the match method.
        /// </summary>
        /// <typeparam name="T">The type of the result.</typeparam>
        /// <param name="task">The task to execute.</param>
        /// <param name="result">The result action.</param>
        /// <param name="error">The error action.</param>
        public static async ValueTask SwitchAsync<T>(this ValueTask<Result<T>> task, Action<T> result, Action<Error> error)
        {
            (await task).Switch(result, error);
        }

        /// <summary>
        /// Asynchronously executes the <see cref="ValueTask"/> then executes the match method and returns the result.
        /// </summary>
        /// <typeparam name="T">The type of the result.</typeparam>
        /// <typeparam name="TResult">The type of the match result.</typeparam>
        /// <param name="task">The task to execute.</param>
        /// <param name="result">The result action.</param>
        /// <param name="error">The error action.</param>
        /// <returns>Returns the result of the match method.</returns>
        public static async ValueTask<TResult> MatchAsync<T, TResult>(this ValueTask<Result<T>> task, Func<T, TResult> result, Func<Error, TResult> error)
        {
            return (await task).Match(result, error);
        }
    }
}