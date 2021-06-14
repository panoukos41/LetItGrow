using LetItGrow.Microservice.Irrigation.Models;
using LetItGrow.Microservice.Irrigation.Requests;
using Microsoft.AspNetCore.SignalR;
using System.Threading.Tasks;

namespace LetItGrow.Microservice.RestApi.Hubs
{
    // Irrigation part of the hub.
    public partial class ApiHub
    {
        [HubMethodName("irrigation:search")]
        public Task<IrrigationModel[]> Irrigation_Search(SearchIrrigations request) =>
            SendRequest(request, Context.ConnectionAborted);
        
        [HubMethodName("irrigation:search-many")]
        public Task<IrrigationModel[]> Irrigation_SearchMany(SearchManyIrrigations request) =>
            SendRequest(request, Context.ConnectionAborted);
    }
}