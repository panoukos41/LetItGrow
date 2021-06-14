using CouchDB.Driver.Exceptions;
using FluentValidation;
using LetItGrow.Identity.Common;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace LetItGrow.Identity.Controllers.Internal
{
    /// <summary>
    /// </summary>
    [ApiController]
    public abstract class BaseController : ControllerBase
    {
        private IMediator? _mediator;

        /// <summary>
        /// The mediator to handle requests.
        /// </summary>
        protected IMediator Mediator => _mediator ??= HttpContext.RequestServices.GetRequiredService<IMediator>();

        /// <summary>
        /// Handle GET, POST, UPDATE, PATCH, DELETE, using <see cref="IMediator"/>
        /// and return an <see cref="OkResult"/>.<br/>
        /// <br/>
        /// If a different exception is raised then a <see cref="StatusCodeResult"/>
        /// with <see cref="StatusCodes.Status500InternalServerError"/> is returned.
        /// </summary>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="request"></param>
        /// <param name="cancellationToken"></param>
        protected async Task<IActionResult> SendRequest<TResult>(
            IRequest<TResult> request,
            CancellationToken cancellationToken)
        {
            try
            {
                return Ok(await Mediator.Send(request, cancellationToken).ConfigureAwait(false));
            }
            catch (Exception ex)
            {
                return ExceptionHandler(ex);
            }
        }

        /// <summary>
        /// Handle GET, POST, UPDATE, PATCH, DELETE, using <see cref="IMediator"/>
        /// and return an <see cref="NoContentResult"/>.<br/>
        /// <br/>
        /// If a different exception is raised then a <see cref="StatusCodeResult"/>
        /// with <see cref="StatusCodes.Status500InternalServerError"/> is returned.
        /// </summary>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="request"></param>
        /// <param name="cancellationToken"></param>
        protected async Task<IActionResult> SendNoContentRequest<TResult>(
            IRequest<TResult> request,
            CancellationToken cancellationToken)
        {
            try
            {
                await Mediator.Send(request, cancellationToken).ConfigureAwait(false);
                return NoContent();
            }
            catch (Exception ex)
            {
                return ExceptionHandler(ex);
            }
        }

        /// <summary>
        /// Handle POST Create, using <see cref="IMediator"/>
        /// and return a <see cref="CreatedAtActionResult"/> with location header.<br/>
        /// <br/>
        /// If a different exception is raised then a <see cref="StatusCodeResult"/>
        /// with <see cref="StatusCodes.Status500InternalServerError"/> is returned.
        /// </summary>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="actionName"></param>
        /// <param name="routeValues"></param>
        /// <param name="request"></param>
        /// <param name="cancellationToken"></param>
        protected async Task<IActionResult> SendCreateRequest<TResult>(
            IRequest<TResult> request,
            string actionName,
            Func<TResult, object> routeValues,
            CancellationToken cancellationToken)
        {
            try
            {
                var result = await Mediator.Send(request, cancellationToken).ConfigureAwait(false);
                return CreatedAtAction(actionName, routeValues(result), result);
            }
            catch (Exception ex)
            {
                return ExceptionHandler(ex);
            }
        }

        /// <summary>
        /// Handle return message based on exception thrown.
        /// </summary>
        private IActionResult ExceptionHandler(Exception exception) => exception switch
        {
            ErrorException ex => StatusCode(
                statusCode: ex.Status,
                value: ex.Error),

            OperationCanceledException => StatusCode(
                statusCode: StatusCodes.Status100Continue,
                value: Errors.Continue),

            ValidationException ex => BadRequest(Errors.Validation with
            {
                Metadata = ex.Errors.ToDictionary(
                    key => key.PropertyName,
                    value => value.ErrorMessage)
            }),

            CouchException ex => StatusCode(
                statusCode: StatusCodes.Status503ServiceUnavailable,
                value: Errors.ServiceUnavailable),

            _ => StatusCode(
                statusCode: StatusCodes.Status500InternalServerError,
                value: Errors.InternalServerError),
        };
    }
}