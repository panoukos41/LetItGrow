﻿using CouchDB.Driver.Exceptions;
using FluentValidation;
using LetItGrow.Microservice.Common;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace LetItGrow.Microservice.RestApi.Controllers.Internal
{
    /// <summary>
    /// v1/[Controller]
    /// </summary>
    [Route("v1/[controller]")]
    [ApiController]
    public abstract class ApiController : ControllerBase
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
        /// The Mediatr handler should throw <see cref="ErrorException"/> for anything wrong and a
        /// <see cref="BadRequestResult"/> will be returned with the <see cref="Common.Error"/> message.<br/>
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
        /// The Mediatr handler should throw <see cref="ErrorException"/> for anything wrong and a
        /// <see cref="BadRequestResult"/> will be returned with the <see cref="Common.Error"/> message.<br/>
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
        /// The Mediatr handler should throw <see cref="ErrorException"/> for anything wrong and a
        /// <see cref="BadRequestResult"/> will be returned with the <see cref="Common.Error"/> message.<br/>
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
            string actionName,
            Func<TResult, object> routeValues,
            IRequest<TResult> request,
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
                statusCode: StatusCodes.Status100Continue),

            ValidationException ex => BadRequest(Errors.Validation with
            {
                Metadata = ex.Errors.ToDictionary(
                    key => key.PropertyName,
                    value => value.ErrorMessage)
            }),

            CouchException ex => StatusCode(
                statusCode: StatusCodes.Status503ServiceUnavailable,
                value: Errors.ServiceUnavailable),

            _ => StatusCode(StatusCodes.Status500InternalServerError),
        };
    }
}