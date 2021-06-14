using MediatR;

namespace LetItGrow.Microservice.Common.Requests
{
    public abstract record BaseCreate<TResponse> : IRequest<TResponse>
    {
    }
}