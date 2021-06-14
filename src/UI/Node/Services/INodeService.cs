using DynamicData;
using LetItGrow.Microservice.Common;
using LetItGrow.Microservice.Common.Models;
using LetItGrow.Microservice.Irrigation.Models;
using LetItGrow.Microservice.Irrigation.Requests;
using LetItGrow.Microservice.Measurement.Models;
using LetItGrow.Microservice.Measurement.Requests;
using LetItGrow.Microservice.Node.Models;
using LetItGrow.Microservice.Node.Requests;
using MediatR;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LetItGrow.UI.Node.Services
{
    public interface INodeService
    {
        /// <summary>
        /// Enumerate all the cached nodes.
        /// </summary>
        IEnumerable<NodeModel> Cache { get; }

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
        /// <see cref="SearchNodes"/> request.
        /// </summary>
        /// <param name="request">The request object.</param>
        /// <returns>
        /// A <see cref="Result{T}"/> that contains the nodes
        /// if the operation was successful.
        /// </returns>
        Task<Result<NodeModel[]>> Search(SearchNodes request);

        /// <summary>
        /// Create a new <see cref="NodeModel"/>.
        /// </summary>
        /// <param name="request">The request object.</param>
        /// <returns>
        /// A <see cref="Result{T}"/> that contains the created node
        /// if the operation was successful.
        /// </returns>
        Task<Result<NodeModel>> Create(CreateNode request);

        /// <summary>
        /// Update an existing <see cref="NodeModel"/>.
        /// </summary>
        /// <param name="request">The request object.</param>
        /// <returns>
        /// A <see cref="Result{T}"/> that contains the updated Id
        /// and Concurrency stamp if the operation was successful.
        /// </returns>
        Task<Result<ModelUpdate>> Update(UpdateNode request);

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
        Task<Result<ModelUpdate>> GroupAdd(GroupAdd request);

        /// <summary>
        /// Removes an existing node from a group.
        /// </summary>
        /// <param name="request">The request object.</param>
        /// <returns>
        /// A <see cref="Result{T}"/> that contains the updated Id
        /// and Concurrency stamp if the operation was successful.
        /// </returns>
        Task<Result<ModelUpdate>> GroupRemove(GroupRemove request);

        /// <summary>
        /// Refreshes a nodes token.
        /// </summary>
        /// <param name="request">The request to refrsh the token.</param>
        /// <returns>A result with the new token and concurrency stamp.</returns>
        Task<Result<RefreshModel>> Refresh(RefreshToken request);

        /// <summary>
        /// Get irrigations for a given node.
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<Result<IrrigationModel[]>> GetIrrigations(SearchIrrigations request);

        /// <summary>
        /// Get irrigations for a given node in batch.
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<Result<IrrigationModel[]>> GetIrrigations(SearchManyIrrigations request);

        /// <summary>
        /// Get measurements for a given node.
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<Result<MeasurementModel[]>> GetMeasurements(SearchMeasurements request);

        /// <summary>
        /// Get measurements for a given node in batch.
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<Result<MeasurementModel[]>> GetMeasurements(SearchManyMeasurements request);
    }
}