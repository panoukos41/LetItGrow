using LetItGrow.Microservice.Common.Models;
using LetItGrow.Microservice.Group.Models;
using LetItGrow.Microservice.Group.Requests;
using MediatR;
using Microsoft.AspNetCore.SignalR;
using System.Threading.Tasks;

namespace LetItGrow.Microservice.RestApi.Hubs
{
    // NodeGroup part of the hub.
    public partial class ApiHub
    {
        [HubMethodName("group:get")]
        public Task<GroupModel> NodeGroup_Get(FindGroup request) =>
            SendRequest(request, Context.ConnectionAborted);

        [HubMethodName("group:search")]
        public Task<GroupModel[]> NodeGroup_Get(SearchGroups request) =>
            SendRequest(request, Context.ConnectionAborted);

        [HubMethodName("group:create")]
        public Task<GroupModel> NodeGroup_Create(CreateGroup request) =>
            SendRequest(request, Context.ConnectionAborted);

        [HubMethodName("group:update")]
        public Task<ModelUpdate> NodeGroup_Update(UpdateGroup request) =>
            SendRequest(request, Context.ConnectionAborted);

        [HubMethodName("group:delete")]
        public Task<Unit> NodeGroup_Delete(DeleteGroup request) =>
            SendRequest(request, Context.ConnectionAborted);
    }
}