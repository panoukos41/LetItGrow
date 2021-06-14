using LetItGrow.Identity.Common;
using System;
using System.Threading.Tasks;

namespace Flurl.Http
{
    public static class FlurlExtensions
    {
        public static async Task<Result<TResult>> SendRequestAsync<TResult>(this Task<TResult> asyncRequest)
        {
            try
            {
                return await asyncRequest.ConfigureAwait(false);
            }
            catch (FlurlHttpException ex)
            {
                return await ex.GetResponseJsonAsync<Error>().ConfigureAwait(false) ??
                    Unkown with { Status = ex.StatusCode ?? 500, Detail = ex.Message };
            }
            catch (Exception ex)
            {
                return Unkown with { Detail = ex.Message };
            }
        }

        private static Error Unkown { get; } = new(
            title: "Unkown error",
            status: 500,
            detail: "");
    }
}