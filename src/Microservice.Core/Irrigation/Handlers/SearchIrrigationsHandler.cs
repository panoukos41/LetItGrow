using LetItGrow.Microservice.Irrigation.Models;
using LetItGrow.Microservice.Irrigation.Requests;
using LetItGrow.Microservice.Abstraction.Stores;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace LetItGrow.Microservice.Irrigation.Handlers
{
    public class SearchIrrigationsHandler : IRequestHandler<SearchIrrigations, IrrigationModel[]>
    {
        private readonly IIrrigationStore store;

        public SearchIrrigationsHandler(IIrrigationStore store)
        {
            this.store = store;
        }

        public Task<IrrigationModel[]> Handle(SearchIrrigations request, CancellationToken cancellationToken)
        {
            return store.Search(request, cancellationToken).AsTask();
        }
    }
}