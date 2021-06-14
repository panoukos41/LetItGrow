using System.Threading.Tasks;

namespace LetItGrow.Microservice.Mqtt.Services
{
    public interface INodeTokenAuthenticator
    {
        ValueTask<bool> Authenticate(string nodeId, string token);
    }
}