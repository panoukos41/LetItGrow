using LetItGrow.Microservice.Common.Models;
using LetItGrow.Microservice.Node.Models;
using LetItGrow.Microservice.Node.Requests;
using MediatR;
using Microsoft.AspNetCore.SignalR;
using System.Threading.Tasks;

namespace LetItGrow.Microservice.RestApi.Hubs
{
    // Node part of the hub.
    public partial class ApiHub
    {
        [HubMethodName("node:search")]
        public Task<NodeModel[]> Node_Get(SearchNodes request) =>
            SendRequest(request, Context.ConnectionAborted);

        [HubMethodName("node:get")]
        public Task<NodeModel> Node_Get(FindNode request) =>
            SendRequest(request, Context.ConnectionAborted);

        [HubMethodName("node:create")]
        public Task<NodeModel> Node_Create(CreateNode request) =>
            SendRequest(request, Context.ConnectionAborted);

        [HubMethodName("node:update")]
        public Task<ModelUpdate> Node_Update(UpdateNode request) =>
            SendRequest(request, Context.ConnectionAborted);

        [HubMethodName("node:delete")]
        public Task<Unit> Node_Delete(DeleteNode request) =>
            SendRequest(request, Context.ConnectionAborted);

        [HubMethodName("node:group-add")]
        public Task<ModelUpdate> Node_GroupAdd(GroupAdd request) =>
            SendRequest(request, Context.ConnectionAborted);

        [HubMethodName("node:group-remove")]
        public Task<ModelUpdate> Node_GroupRemove(GroupRemove request) =>
            SendRequest(request, Context.ConnectionAborted);

        [HubMethodName("node:refresh")]
        public Task<RefreshModel> Node_RefreshToken(RefreshToken request) =>
            SendRequest(request, Context.ConnectionAborted);
    }
}