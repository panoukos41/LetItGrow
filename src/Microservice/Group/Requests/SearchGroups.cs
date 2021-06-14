using LetItGrow.Microservice.Common.Requests;
using LetItGrow.Microservice.Group.Models;

namespace LetItGrow.Microservice.Group.Requests
{
    /// <summary>
    /// A request to search multiple node groups depending on criteria.<br/>
    /// It also contains search settings.
    /// </summary>
    public record SearchGroups : BaseSearch<GroupModel>
    {
    }
}