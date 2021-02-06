using DynamicData;
using LetItGrow.Microservice.Data;
using LetItGrow.Microservice.Data.Measurements.Models;
using LetItGrow.Microservice.Data.Measurements.Requests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LetItGrow.UI.Services
{
    public interface IMeasurementService
    {
        // todo: Implement IMeasurementsService
        Task<Result<MeasurementModel[]>> Search(SearchMeasurements request);
    }
}
