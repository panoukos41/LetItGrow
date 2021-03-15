using LetItGrow.Microservice.Data.Groups.Models;
using MediatR;

namespace LetItGrow.Microservice.Data.Groups.Requests
{
    /// <summary>
    /// A request to search multiple node groups depending on criteria.<br/>
    /// It also contains search settings.
    /// </summary>
    public record FindGroups : IRequest<GroupModel[]>
    {
        public FindGroups()
        {
        }
    }
}