using LetItGrow.Microservice.Data.NodeGroups.Models;
using MediatR;
using System.Collections.Generic;

namespace LetItGrow.Microservice.Data.NodeGroups.Requests
{
    /// <summary>
    /// A request to search multiple node groups depending on criteria.<br/>
    /// It also contains search settings.
    /// </summary>
    public record GetNodeGroups : IRequest<List<NodeGroupModel>>
    {
        public GetNodeGroups()
        {
        }
    }
}