﻿using LetItGrow.Microservice.Data.NodeAuths.Models;
using LetItGrow.Microservice.Data.Nodes.Models;
using MediatR;
using System.Text.Json.Serialization;

namespace LetItGrow.Microservice.Data.NodeAuths.Requests
{
    public record CreateNodeAuth : IRequest<NodeAuthModel>
    {
        [JsonPropertyName("nodeId")]
        public string NodeId { get; init; } = string.Empty;

        public CreateNodeAuth()
        {
        }

        public CreateNodeAuth(string nodeId)
        {
            NodeId = nodeId;
        }

        public CreateNodeAuth(NodeModel node)
        {
            NodeId = node.Id;
        }
    }
}