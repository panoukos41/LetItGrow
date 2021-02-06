using DynamicData;
using LetItGrow.Microservice.Data;
using LetItGrow.Microservice.Data.Nodes.Models;
using LetItGrow.Microservice.Data.Nodes.Requests;
using MediatR;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LetItGrow.UI.Services
{
    public interface INodeService
    {
        /// <summary>
        /// Returns a filtered stream of cache changes preceded with the initial filtered state.
        /// </summary>
        /// <param name="predicate">The result will be filtered using the specified predicate.</param>
        /// <returns>An observable that emits the change set.</returns>
        IObservable<IChangeSet<NodeModel, string>> Connect(Func<NodeModel, bool>? predicate = null);

        /// <summary>
        /// Watches updates for a single value matching the specified key.
        /// Call <see cref="Get(string)"/> before to be sure that the node
        /// you are about to watch has been loaded and cached.
        /// </summary>
        /// <param name="id">The id of the node to watch.</param>
        /// <returns>An observable which emits the object value.</returns>
        IObservable<NodeModel> Watch(string id);

        /// <summary>
        /// Get a node for the specific id.
        /// </summary>
        /// <param name="id">The node id.</param>
        /// <returns>
        /// A <see cref="Result{T}"/> that contains the node
        /// if the operation was successful.
        /// </returns>
        ValueTask<Result<NodeModel>> Get(string id);

        /// <summary>
        /// Get all nodes with conditions and settings definde by the
        /// <see cref="GetNodes"/> request.
        /// </summary>
        /// <param name="request">The request object.</param>
        /// <returns>
        /// A <see cref="Result{T}"/> that contains the nodes
        /// if the operation was successful.
        /// </returns>
        Task<Result<List<NodeModel>>> GetAll(GetNodes request);

        /// <summary>
        /// Create a new <see cref="IrrigationNodeModel"/>.
        /// </summary>
        /// <param name="request">The request object.</param>
        /// <returns>
        /// A <see cref="Result{T}"/> that contains the created node
        /// if the operation was successful.
        /// </returns>
        Task<Result<IrrigationNodeModel>> Create(CreateIrrigationNode request);

        /// <summary>
        /// Create a new <see cref="MeasurementNodeModel"/>.
        /// </summary>
        /// <param name="request">The request object.</param>
        /// <returns>
        /// A <see cref="Result{T}"/> that contains the created node
        /// if the operation was successful.
        /// </returns>
        Task<Result<MeasurementNodeModel>> Create(CreateMeasurementNode request);

        /// <summary>
        /// Update an existing <see cref="IrrigationNodeModel"/>.
        /// </summary>
        /// <param name="request">The request object.</param>
        /// <returns>
        /// A <see cref="Result{T}"/> that contains the updated Id
        /// and Concurrency stamp if the operation was successful.
        /// </returns>
        Task<Result<NodeModelUpdate>> Update(UpdateIrrigationNode request);

        /// <summary>
        /// Update an existing <see cref="MeasurementNodeModel"/>.
        /// </summary>
        /// <param name="request">The request object.</param>
        /// <returns>
        /// A <see cref="Result{T}"/> that contains the updated Id
        /// and Concurrency stamp if the operation was successful.
        /// </returns>
        Task<Result<NodeModelUpdate>> Update(UpdateMeasurementNode request);

        /// <summary>
        /// Delete an existing node.
        /// </summary>
        /// <param name="request">The reuquest object.</param>
        /// <returns>
        /// A <see cref="Result{T}"/> that indicates if the operation was successful.
        /// </returns>
        Task<Result<Unit>> Delete(DeleteNode request);

        /// <summary>
        /// Add an existing node to an existing group.
        /// </summary>
        /// <param name="request">The request object.</param>
        /// <returns>
        /// A <see cref="Result{T}"/> that contains the updated Id
        /// and Concurrency stamp if the operation was successful.
        /// </returns>
        Task<Result<NodeModelUpdate>> AddToGroup(AddNodeToGroup request);

        /// <summary>
        /// Removes an existing node from a group.
        /// </summary>
        /// <param name="request">The request object.</param>
        /// <returns>
        /// A <see cref="Result{T}"/> that contains the updated Id
        /// and Concurrency stamp if the operation was successful.
        /// </returns>
        Task<Result<NodeModelUpdate>> RemoveFromGroup(RemoveNodeFromGroup request);
    }
}