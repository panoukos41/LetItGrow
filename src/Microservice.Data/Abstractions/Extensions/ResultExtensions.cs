using System;
using System.Threading.Tasks;

namespace LetItGrow.Microservice.Data
{
    public static class ResultExtensions
    {
        /// <summary>
        /// Asynchronously executes the <see cref="Task"/> then executes the OnSuccess method.
        /// </summary>
        /// <typeparam name="T">The type of the result.</typeparam>
        /// <param name="task">The task to execute.</param>
        /// <param name="success">The result action.</param>
        public static async Task OnSuccess<T>(this Task<Result<T>> task, Action<T> success)
        {
            (await task).OnSuccess(success);
        }

        /// <summary>
        /// Asynchronously executes the <see cref="Task"/> then executes the OnError method.
        /// </summary>
        /// <typeparam name="T">The type of the result.</typeparam>
        /// <param name="task">The task to execute.</param>
        /// <param name="error">The error action.</param>
        public static async Task OnError<T>(this Task<Result<T>> task, Action<Error> error)
        {
            (await task).OnError(error);
        }

        /// <summary>
        /// Asynchronously executes the <see cref="Task"/> then executes the match method.
        /// </summary>
        /// <typeparam name="T">The type of the result.</typeparam>
        /// <param name="task">The task to execute.</param>
        /// <param name="result">The result action.</param>
        /// <param name="error">The error action.</param>
        public static async Task MatchAsync<T>(this Task<Result<T>> task, Action<T> result, Action<Error> error)
        {
            (await task).Match(result, error);
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
        /// Asynchronously executes the <see cref="ValueTask"/> then executes the OnSuccess method.
        /// </summary>
        /// <typeparam name="T">The type of the result.</typeparam>
        /// <param name="task">The task to execute.</param>
        /// <param name="success">The result action.</param>
        public static async ValueTask OnSuccess<T>(this ValueTask<Result<T>> task, Action<T> success)
        {
            (await task).OnSuccess(success);
        }

        /// <summary>
        /// Asynchronously executes the <see cref="ValueTask"/> then executes the OnError method.
        /// </summary>
        /// <typeparam name="T">The type of the result.</typeparam>
        /// <param name="task">The task to execute.</param>
        /// <param name="error">The error action.</param>
        public static async ValueTask OnError<T>(this ValueTask<Result<T>> task, Action<Error> error)
        {
            (await task).OnError(error);
        }

        /// <summary>
        /// Asynchronously executes the <see cref="ValueTask"/> then executes the match method.
        /// </summary>
        /// <typeparam name="T">The type of the result.</typeparam>
        /// <param name="task">The task to execute.</param>
        /// <param name="result">The result action.</param>
        /// <param name="error">The error action.</param>
        public static async ValueTask MatchAsync<T>(this ValueTask<Result<T>> task, Action<T> result, Action<Error> error)
        {
            (await task).Match(result, error);
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