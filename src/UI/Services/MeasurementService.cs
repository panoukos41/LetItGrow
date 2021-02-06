using DynamicData;
using LetItGrow.Microservice.Data;
using LetItGrow.Microservice.Data.Measurements.Models;
using LetItGrow.Microservice.Data.Measurements.Requests;
using LetItGrow.UI.Services.Internal;
using Microsoft.AspNetCore.SignalR.Client;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LetItGrow.UI.Services
{
    public class MeasurementService : HubServiceBase, IMeasurementService
    {
        private readonly SourceCache<(SearchMeasurements query, MeasurementModel[] result), SearchMeasurements> source = new(x => x.query);

        public MeasurementService(HubConnection hub) : base(hub)
        {
        }

        public Task<Result<MeasurementModel[]>> Search(SearchMeasurements request)
        {
            return Handle("", request);
        }
    }
}