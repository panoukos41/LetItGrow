using LetItGrow.Microservice.Measurement.Models;
using LetItGrow.Microservice.Measurement.Requests;
using Microsoft.AspNetCore.SignalR;
using System.Threading.Tasks;

namespace LetItGrow.Microservice.RestApi.Hubs
{
    // Measurement part of the hub.
    public partial class ApiHub
    {
        [HubMethodName("measurement:search")]
        public Task<MeasurementModel[]> Measurement_Search(SearchMeasurements request) =>
            SendRequest(request, Context.ConnectionAborted);

        [HubMethodName("measurement:search-many")]
        public Task<MeasurementModel[]> Measurement_SearchMany(SearchManyMeasurements request) =>
            SendRequest(request, Context.ConnectionAborted);
    }
}