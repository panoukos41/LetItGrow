using LetItGrow.Web.Data.Measurements.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LetItGrow.Web.Data.Measurements.Requests
{
    public record SearchMeasurements : IRequest<List<MeasurementModel>>
    {
        //todo: search measurements
    }
}
