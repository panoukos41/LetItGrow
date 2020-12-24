using System;
using System.Threading.Tasks;

namespace LetItGrow.Web.Data.Common
{
    public static class ResultExtensions
    {
        public static async Task MatchAsync<T>(this Task<Result<T>> task, Action<T> result, Action<Error> error)
        {
            (await task).Match(result, error);
        }

        public static async Task<TResult> MatchAsync<T, TResult>(this Task<Result<T>> task, Func<T, TResult> result, Func<Error, TResult> error)
        {
            return (await task).Match(result, error);
        }

        public static async ValueTask MatchAsync<T>(this ValueTask<Result<T>> task, Action<T> result, Action<Error> error)
        {
            (await task).Match(result, error);
        }

        public static async ValueTask<TResult> MatchAsync<T, TResult>(this ValueTask<Result<T>> task, Func<T, TResult> result, Func<Error, TResult> error)
        {
            return (await task).Match(result, error);
        }
    }
}