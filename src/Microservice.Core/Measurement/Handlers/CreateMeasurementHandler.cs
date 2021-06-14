using LetItGrow.Microservice.Measurement.Requests;
using LetItGrow.Microservice.Abstraction.Stores;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace LetItGrow.Microservice.Measurement.Handlers
{
    public class CreateMeasurementHandler : IRequestHandler<CreateMeasurement, Unit>
    {
        private readonly IMeasurementStore store;

        public CreateMeasurementHandler(IMeasurementStore store)
        {
            this.store = store;
        }

        public async Task<Unit> Handle(CreateMeasurement request, CancellationToken cancellationToken)
        {
            await store.Create(request, cancellationToken);

            return Unit.Value;
        }
    }
}