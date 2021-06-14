using CouchDB.Driver.Exceptions;
using FluentValidation;
using LetItGrow.Microservice.Common;
using MediatR;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Linq;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;

namespace LetItGrow.Microservice.RestApi.Hubs.Internal
{
    public abstract class HubBase : Hub
    {
        protected IMediator Mediator { get; }

        protected HubBase(IMediator mediator) =>
            Mediator = mediator;

        protected async Task<TResult> SendRequest<TResult>(IRequest<TResult> request, CancellationToken cancellationToken)
        {
            try
            {
                return await Mediator.Send(request, cancellationToken);
            }
            catch (Exception ex)
            {
                throw HandleExceltpions(ex);
            }
        }

        protected Task PublishNotification(INotification notification, CancellationToken cancellationToken)
        {
            return Mediator.Publish(notification, cancellationToken);
        }

        protected static Exception HandleExceltpions(Exception exception) => exception switch
        {
            ErrorException ex => NewHubException(ex.Error),

            OperationCanceledException => NewHubException(Errors.Continue),

            ValidationException ex => NewHubException(Errors.Validation with
            {
                Metadata = ex.Errors.ToDictionary(
                    key => key.PropertyName,
                    value => value.ErrorMessage)
            }),

            CouchException ex => NewHubException(Errors.ServiceUnavailable),

            _ => NewHubException(Errors.InternalServerError),
        };

        protected static Exception NewHubException(Error error) =>
            new HubException("~" + JsonSerializer.Serialize(error));
    }
}