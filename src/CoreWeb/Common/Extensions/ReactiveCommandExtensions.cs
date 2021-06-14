using System;
using System.Reactive;
using System.Reactive.Linq;

namespace ReactiveUI
{
    public static class ReactiveCommandExtensions
    {
        /// <summary>
        /// A utility method that will pipe an Observable to an ICommand (i.e. it will first
        /// call its CanExecute with the provided value, then if the command can be executed,
        /// Execute() will be called).
        /// </summary>
        /// <typeparam name="TParam">The type of the parameter.</typeparam>
        /// <typeparam name="TResult">The type of the result.</typeparam>
        /// <param name="command">The command to execute.</param>
        /// <param name="param">The parameter to pass.</param>
        /// <returns>An object that when disposes, disconnects the Observable from the command.</returns>
        /// <remarks>
        /// This is equivalent to calling <see cref="Observable.Return{TResult}(TResult)"/>.InvokeCommand(commad);
        /// </remarks>
        public static IDisposable Invoke<TParam, TResult>(this ReactiveCommandBase<TParam, TResult> command, TParam param)
        {
            return Observable.Return(param).InvokeCommand(command);
        }

        /// <summary>
        /// A utility method that will pipe an Observable to an ICommand (i.e. it will first
        /// call its CanExecute with the provided value, then if the command can be executed,
        /// Execute() will be called).
        /// </summary>
        /// <typeparam name="TParam">The type of the parameter.</typeparam>
        /// <param name="command">The command to execute.</param>
        /// <param name="param">The parameter to pass.</param>
        /// <returns>An object that when disposes, disconnects the Observable from the command.</returns>
        /// <remarks>
        /// This is equivalent to calling <see cref="Observable.Return{TResult}(TResult)"/>.InvokeCommand(commad);
        /// </remarks>
        public static IDisposable Invoke<TParam>(this ReactiveCommandBase<TParam, Unit> command, TParam param)
        {
            return Observable.Return(param).InvokeCommand(command);
        }

        /// <summary>
        /// A utility method that will pipe an Observable to an ICommand (i.e. it will first
        /// call its CanExecute with the provided value, then if the command can be executed,
        /// Execute() will be called).
        /// </summary>
        /// <typeparam name="TResult">The type of the result.</typeparam>
        /// <param name="command">The command to execute.</param>
        /// <returns>An object that when disposes, disconnects the Observable from the command.</returns>
        /// <remarks>
        /// This is equivalent to calling <see cref="Observable.Return{TResult}(TResult)"/>.InvokeCommand(commad);
        /// </remarks>
        public static IDisposable Invoke<TResult>(this ReactiveCommandBase<Unit, TResult> command)
        {
            return Observable.Return(Unit.Default).InvokeCommand(command);
        }

        /// <summary>
        /// A utility method that will pipe an Observable to an ICommand (i.e. it will first
        /// call its CanExecute with the provided value, then if the command can be executed,
        /// Execute() will be called).
        /// </summary>
        /// <param name="command">The command to execute.</param>
        /// <returns>An object that when disposes, disconnects the Observable from the command.</returns>
        /// <remarks>
        /// This is equivalent to calling <see cref="Observable.Return{TResult}(TResult)"/>.InvokeCommand(commad);
        /// </remarks>
        public static IDisposable Invoke(this ReactiveCommandBase<Unit, Unit> command)
        {
            return Observable.Return(Unit.Default).InvokeCommand(command);
        }
    }
}