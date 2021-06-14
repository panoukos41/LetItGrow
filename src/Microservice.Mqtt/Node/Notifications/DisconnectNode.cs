using MediatR;

namespace LetItGrow.Microservice.Node.Notifications
{
    public class DisconnectNode : INotification
    {
        public string NodeId { get; }

        public DisconnectNode(string nodeId)
        {
            NodeId = nodeId;
        }
    }
}