using MediatR;

namespace LetItGrow.Microservice.Common.Requests
{
    public abstract record BaseSearch<TResponse> : IRequest<TResponse[]>
    {
    }
}