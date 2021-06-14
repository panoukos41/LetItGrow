using System.Threading;
using System.Threading.Tasks;

namespace Microservice.Worker
{
    public interface IIrrigator
    {
        public Task Irrigate(CancellationToken stoppingToken);
    }
}