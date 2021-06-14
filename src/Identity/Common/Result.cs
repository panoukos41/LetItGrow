using OneOf;

namespace LetItGrow.Identity.Common
{
    public class Result<TResult> : OneOfBase<TResult, Error>
    {
        protected Result(OneOf<TResult, Error> _) : base(_)
        {
        }

        public static implicit operator Result<TResult>(TResult _) => new(_);

        public static implicit operator Result<TResult>(Error _) => new(_);
    }
}