using LetItGrow.Microservice.Abstraction.Stores;
using LetItGrow.Microservice.Irrigation.Models;
using LetItGrow.Microservice.Irrigation.Requests;
using MediatR;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace LetItGrow.Microservice.Irrigation.Handlers
{
    public class SearchManyIrrigationsHandler : IRequestHandler<SearchManyIrrigations, IrrigationModel[]>
    {
        private readonly IIrrigationStore store;

        public SearchManyIrrigationsHandler(IIrrigationStore store)
        {
            this.store = store;
        }

        public Task<IrrigationModel[]> Handle(SearchManyIrrigations request, CancellationToken cancellationToken)
        {
            return store.SearchMany(
                cancellationToken: cancellationToken,
                request: request.NodeIds
                    .Select(id => new SearchIrrigations
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