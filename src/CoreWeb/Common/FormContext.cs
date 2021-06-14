using FluentValidation;
using Microsoft.AspNetCore.Components.Forms;
using ReactiveUI;
using System;
using System.Reactive;
using System.Reactive.Subjects;
using System.Threading.Tasks;

namespace LetItGrow.CoreWeb.Common
{
    public class FormContext<TRequest, TValidator, TResult>
        where TRequest : new()
        where TValidator : IValidator<TRequest>, new()
    {
        private readonly Subject<TResult> _whenResult = new();

        /// <summary>
        /// The Request object.
        /// </summary>
        public TRequest Request { get; private set; }

        /// <summary>
        /// The validator that is being created for the Request object.
        /// </summary>
        public TValidator Validator { get; private set; }

        /// <summary>
        /// The edit context that is created for the Request object.
        /// </summary>
        public EditContext EditContext { get; private set; }

        /// <summary>
        /// Observable that ticks when the command has finished executing.
        /// </summary>
        public IObservable<TResult> WhenResult => _whenResult;

        /// <summary>
        /// The reactive command that is being created.
        /// </summary>
        public ReactiveCommand<Unit, Unit> Command { get; }

        public FormContext(
            Func<TRequest, Task<TResult>> exec,
            Action<TResult> result,
            Func<bool>? canExec = null)
            : this(new(), exec, result, canExec)
        {
        }

        public FormContext(
            TRequest request,
            Func<TRequest, Task<TResult>> exec,
            Action<TResult> result,
            Func<bool>? canExec = null)
        {
            Request = request;
            Validator = new();
            EditContext = new(Request!);
            WhenResult.Subscribe(result);

            Command = ReactiveCommand.CreateFromTask(
                execute: async () =>
                {
                    if (EditContext.Validate() is false) return;

                    if (canExec is not null && canExec() is false) return;

                    var result = await exec(Request);
                    _whenResult.OnNext(result);
                });
        }

        /// <summary>
        /// Reset the request object.
        /// </summary>
        public void Reset() => Reset(new());

        public void Reset(TRequest request)
        {
            Request = request;
            Validator = new();
            EditContext = new(Request!);
        }

        public static implicit operator TRequest(FormContext<TRequest, TValidator, TResult> form) => form.Request;

        public static implicit operator TValidator(FormContext<TRequest, TValidator, TResult> form) => form.Validator;

        public static implicit operator EditContext(FormContext<TRequest, TValidator, TResult> form) => form.EditContext;

        public static implicit operator ReactiveCommand<Unit, Unit>(FormContext<TRequest, TValidator, TResult> form) => form.Command;
    }
}