using MediatR;

namespace LetItGrow.Microservice.Data.Nodes.Requests
{
    public record GetNodeSettings : IRequest<string>
    {
        public string Id { get; set; } = string.Empty;

        public GetNodeSettings()
        {
        }

        public GetNodeSettings(string id)
        {
            Id = id;
        }
    }
}