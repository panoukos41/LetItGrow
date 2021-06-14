using LetItGrow.Microservice.Measurement.Models;
using LetItGrow.Microservice.Measurement.Requests;
using System.Threading;
using System.Threading.Tasks;

namespace LetItGrow.Microservice.Abstraction.Stores
{
    public interface IMeasurementStore
    {
        ValueTask<MeasurementModel[]> Search(SearchMeasurements request, CancellationToken cancellationToken);
        
        ValueTask<MeasurementModel[]> SearchMany(SearchMeasurements[] request, CancellationToken cancellationToken);

        Task<MeasurementModel> Create(CreateMeasurement request, CancellationToken cancellationToken);
    }
}