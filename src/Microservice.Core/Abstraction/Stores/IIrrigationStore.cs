using LetItGrow.Microservice.Irrigation.Models;
using LetItGrow.Microservice.Irrigation.Requests;
using System.Threading;
using System.Threading.Tasks;

namespace LetItGrow.Microservice.Abstraction.Stores
{
    public interface IIrrigationStore
    {
        ValueTask<IrrigationModel[]> Search(SearchIrrigations request, CancellationToken cancellationToken);
        
        ValueTask<IrrigationModel[]> SearchMany(SearchIrrigations[] request, CancellationToken cancellationToken);

        Task<IrrigationModel> Create(CreateIrrigation request, CancellationToken cancellationToken);
    }
}