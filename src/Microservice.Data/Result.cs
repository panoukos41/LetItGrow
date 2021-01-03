using System;

namespace LetItGrow.Microservice.Data
{
    /// <summary>
    /// A union that will either return the desired value or an error.
    /// Match will execute the appropriate code.
    /// </summary>
    /// <typeparam name="T">The type of the result if it's not errored.</typeparam>
    public abstract class Result<T>
    {
        protected Result()
        {
        }

        public abstract TResult Match<TResult>(Func<T, TResult> result, Func<Error, TResult> error);

        public abstract void Match(Action<T> result, Action<Error>? error);

        public static implicit operator Result<T>(T value) =>
            new ResultCase(value);

        public static implicit operator Result<T>(Error value) =>
            new ErrorCase(value);

        /// <summary>
        /// The result you get for your own value.
        /// </summary>
        public sealed class ResultCase : Result<T>
        {
            private readonly T value;

            public ResultCase(T value) =>
                this.value = value;

            public override TResult Match<TResult>(Func<T, TResult> result, Func<Error, TResult> error) =>
                result(value);

            public override void Match(Action<T> result, Action<Error>? error) =>
                result(value);
        }

        /// <summary>
        /// The result you get for the error value.
        /// </summary>
        public sealed class ErrorCase : Result<T>
        {
            private readonly Error value;

            public ErrorCase(Error value) =>
                this.value = value;

            public override TResult Match<TResult>(Func<T, TResult> result, Func<Error, TResult> error) =>
                error.Invoke(value);

            public override void Match(Action<T> result, Action<Error>? error) =>
                error?.Invoke(value);
        }
    }
}