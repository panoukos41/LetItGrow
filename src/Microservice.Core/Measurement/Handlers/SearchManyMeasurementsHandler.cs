using LetItGrow.Microservice.Abstraction.Stores;
using LetItGrow.Microservice.Measurement.Models;
using LetItGrow.Microservice.Measurement.Requests;
using MediatR;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace LetItGrow.Microservice.Measurement.Handlers
{
    public class SearchManyMeasurementsHandler : IRequestHandler<SearchManyMeasurements, MeasurementModel[]>
    {
        private readonly IMeasurementStore store;

        public SearchManyMeasurementsHandler(IMeasurementStore store)
        {
            this.store = store;
        }

        public Task<MeasurementModel[]> Handle(SearchManyMeasurements request, CancellationToken cancellationToken)
        {
            return store.SearchMany(
                cancellationToken: cancellationToken,
                request: request.NodeIds
                    .Select(id => new SearchMeasurements
                    {
                        NodeId = id,
                        StartDate = request.StartDate,
                        EndDate = request.EndDate
                    })
                    .ToArray())
                .AsTask();
        }
    }
}