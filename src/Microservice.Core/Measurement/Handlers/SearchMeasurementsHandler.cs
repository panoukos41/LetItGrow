using LetItGrow.Microservice.Measurement.Models;
using LetItGrow.Microservice.Measurement.Requests;
using LetItGrow.Microservice.Abstraction.Stores;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace LetItGrow.Microservice.Measurement.Handlers
{
    public class SearchMeasurementsHandler : IRequestHandler<SearchMeasurements, MeasurementModel[]>
    {
        private readonly IMeasurementStore store;

        public SearchMeasurementsHandler(IMeasurementStore store)
        {
            this.store = store;
        }

        public Task<MeasurementModel[]> Handle(SearchMeasurements request, CancellationToken cancellationToken)
        {
            return store.Search(request, cancellationToken).AsTask();
        }
    }
}