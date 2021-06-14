using LetItGrow.Microservice.Common.Requests;
using LetItGrow.Microservice.Group.Models;

namespace LetItGrow.Microservice.Group.Requests
{
    /// <summary>
    /// A request to delete a node group.
    /// </summary>
    public record DeleteGroup : BaseDelete
    {
        public DeleteGroup()
        {
        }

        public DeleteGroup(string id, string concurrencyStamp)
        {
            Id = id;
            ConcurrencyStamp = concurrencyStamp;
        }

        public DeleteGroup(GroupModel group)
        {
            Id = group.Id;
            ConcurrencyStamp = group.ConcurrencyStamp;
        }
    }
}