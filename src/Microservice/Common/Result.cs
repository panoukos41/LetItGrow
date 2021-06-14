using OneOf;
using System.Threading.Tasks;

namespace LetItGrow.Microservice.Common
{
    /// <summary>
    /// A class that represents either a success <typeparamref name="T"/> (T0) or an <see cref="Error"/> (T1).
    /// </summary>
    /// <typeparam name="T">The type of the success value.</typeparam>
    public class Result<T> : OneOfBase<T, Error> //Result<T, Error>
    {
        public Result(T success) : base(success)
        {
        }

        public Result(Error error) : base(error)
        {
        }

        protected Result(OneOf<T, Error> _) : base(_)
        {
        }

        public static Task<Result<T>> Task(T success) =>
            System.Threading.Tasks.Task.FromResult<Result<T>>(new(success));

        public static Task<Result<T>> Task(Error error) =>
            System.Threading.Tasks.Task.FromResult<Result<T>>(new(error));

        public static implicit operator Result<T>(T _) => new(_);

        public static implicit operator Result<T>(Error _) => new(_);
    }
}