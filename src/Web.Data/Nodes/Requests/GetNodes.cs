﻿using LetItGrow.Web.Data.Nodes.Models;
using MediatR;
using System.Collections.Generic;

namespace LetItGrow.Web.Data.Nodes.Requests
{
    /// <summary>
    /// A request to search multiple nodes depending on criteria.<br/>
    /// It also contains search settings.
    /// </summary>
    public record GetNodes : IRequest<List<NodeModel>>
    {
        /// <summary>
        /// The type of the nodes to get.
        /// </summary>
        public string? Type { get; init; }

        /// <summary>
        /// Load the nodes and include the groups as well, by default this value is false.
        /// </summary>
        public bool IncludeGroups { get; init; }
    }
}