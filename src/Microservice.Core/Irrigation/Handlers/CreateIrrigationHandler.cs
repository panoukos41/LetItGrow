using LetItGrow.Microservice.Irrigation.Requests;
using LetItGrow.Microservice.Abstraction.Stores;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace LetItGrow.Microservice.Irrigation.Handlers
{
    public class CreateIrrigationHandler : IRequestHandler<CreateIrrigation, Unit>
    {
        private readonly IIrrigationStore store;

        public CreateIrrigationHandler(IIrrigationStore store)
        {
            this.store = store;
        }

        public async Task<Unit> Handle(CreateIrrigation request, CancellationToken cancellationToken)
        {
            await store.Create(request, cancellationToken);

            return Unit.Value;
        }
    }
}