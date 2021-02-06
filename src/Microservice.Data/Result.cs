using System;

namespace LetItGrow.Microservice.Data
{
    /// <summary>
    /// A class that represents either a success or an <see cref="Error"/>.
    /// </summary>
    /// <typeparam name="T">The type of the success value.</typeparam>
    public abstract class Result<T> : Result<T, Error>
    {
        /// <summary>
        /// Convert success values to ResultCase.
        /// </summary>
        public static implicit operator Result<T>(T value) => new Ok<T>(value);

        /// <summary>
        /// Convert error values to ErrorCase.
        /// </summary>
        public static implicit operator Result<T>(Error value) => new Er<T>(value);
    }

    /// <summary>
    /// The success class.
    /// </summary>
    public class Ok<T> : Result<T>
    {
        /// <summary>
        /// Initialize a new success with a value.
        /// </summary>
        public Ok(T value) => Success = value;

        /// <inheritdoc/>
        public override T? Success { get; }

        /// <inheritdoc/>
        public override Error? Error { get; } = default;

        /// <inheritdoc/>
        public override void Match(Action<T> success, Action<Error> error) => success(Success!);

        /// <inheritdoc/>
        public override TResult Match<TResult>(Func<T, TResult> success, Func<Error, TResult> error) => success(Success!);

        /// <inheritdoc/>
        public override void OnError(Action<Error> error) { }

        /// <inheritdoc/>
        public override void OnSuccess(Action<T> success) => success(Success!);
    }

    /// <summary>
    /// The error class.
    /// </summary>
    public class Er<T> : Result<T>
    {
        /// <summary>
        /// Initialize a new error with a value.
        /// </summary>
        public Er(Error error) => Error = error;

        /// <inheritdoc/>
        public override T? Success { get; } = default;

        /// <inheritdoc/>
        public override Error? Error { get; }

        /// <inheritdoc/>
        public override void Match(Action<T> success, Action<Error> error) => error(Error!);

        /// <inheritdoc/>
        public override TResult Match<TResult>(Func<T, TResult> success, Func<Error, TResult> error) => error(Error!);

        /// <inheritdoc/>
        public override void OnError(Action<Error> error) => error(Error!);

        /// <inheritdoc/>
        public override void OnSuccess(Action<T> success) { }
    }

    /// <summary>
    /// A class that represents either a success or an error.
    /// </summary>
    /// <typeparam name="T">The type of the success value.</typeparam>
    /// <typeparam name="E">The type of the error value.</typeparam>
    public abstract class Result<T, E>
    {
        /// <summary>
        /// The success value, if available.
        /// </summary>
        public abstract T? Success { get; }

        /// <summary>
        /// The error value, if available.
        /// </summary>
        public abstract E? Error { get; }

        /// <summary>
        /// Execute this action only when a success is available.
        /// </summary>
        /// <param name="success">The success action.</param>
        public abstract void OnSuccess(Action<T> success);

        /// <summary>
        /// Execute this action only when an error is availlable.
        /// </summary>
        /// <param name="error"></param>
        public abstract void OnError(Action<E> error);

        /// <summary>
        /// Executes the success or error action.
        /// </summary>
        /// <param name="success">The success action.</param>
        /// <param name="error">The error action.</param>
        public abstract void Match(Action<T> success, Action<E> error);

        /// <summary>
        /// Executes the success or error func and returns a common value.
        /// </summary>
        /// <typeparam name="TResult">The type of the value to return.</typeparam>
        /// <param name="success">The success func.</param>
        /// <param name="error">The error func.</param>
        /// <returns>A common value.</returns>
        public abstract TResult Match<TResult>(Func<T, TResult> success, Func<E, TResult> error);

        /// <summary>
        /// Convert success values to ResultCase.
        /// </summary>
        public static implicit operator Result<T, E>(T value) => new Ok<T, E>(value);

        /// <summary>
        /// Convert error values to ErrorCase.
        /// </summary>
        public static implicit operator Result<T, E>(E value) => new Er<T, E>(value);
    }

    /// <summary>
    /// The success class.
    /// </summary>
    public sealed class Ok<T, E> : Result<T, E>
    {
        /// <summary>
        /// Initialize a new success with a value.
        /// </summary>
        public Ok(T value) => Success = value;

        /// <inheritdoc/>
        public override T? Success { get; }

        /// <inheritdoc/>
        public override E? Error { get; } = default;

        /// <inheritdoc/>
        public override void OnSuccess(Action<T> success) => success(Success!);

        /// <inheritdoc/>
        public override void OnError(Action<E> error) { }

        /// <inheritdoc/>
        public override void Match(Action<T> success, Action<E>? error = null) => success(Success!);

        /// <inheritdoc/>
        public override TResult Match<TResult>(Func<T, TResult> success, Func<E, TResult> error) => success(Success!);
    }

    /// <summary>
    /// The error class.
    /// </summary>
    public sealed class Er<T, E> : Result<T, E>
    {
        /// <summary>
        /// Initialize a new error with a value.
        /// </summary>
        public Er(E error) => Error = error;

        /// <inheritdoc/>
        public override T? Success { get; } = default;

        /// <inheritdoc/>
        public override E? Error { get; }

        /// <inheritdoc/>
        public override void OnSuccess(Action<T> success) { }

        /// <inheritdoc/>
        public override void OnError(Action<E> error) => error(Error!);

        /// <inheritdoc/>
        public override void Match(Action<T> success, Action<E> error) => error(Error!);

        /// <inheritdoc/>
        public override TResult Match<TResult>(Func<T, TResult> success, Func<E, TResult> error) => error(Error!);
    }
}