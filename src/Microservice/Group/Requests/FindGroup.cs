using LetItGrow.Microservice.Common.Requests;
using LetItGrow.Microservice.Group.Models;

namespace LetItGrow.Microservice.Group.Requests
{
    /// <summary>
    /// A request to get a node group.
    /// </summary>
    public record FindGroup : BaseFind<GroupModel>
    {
        public FindGroup()
        {
        }

        public FindGroup(string id)
        {
            Id = id;
        }
    }
}