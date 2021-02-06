using DynamicData;
using LetItGrow.Microservice.Data;
using LetItGrow.Microservice.Data.NodeGroups.Models;
using LetItGrow.Microservice.Data.NodeGroups.Requests;
using MediatR;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LetItGrow.UI.Services
{
    public interface INodeGroupService
    {
        /// <summary>
        /// Returns a filtered stream of cache changes preceded with the initial filtered state.
        /// </summary>
        /// <param name="predicate">The result will be filtered using the specified predicate.</param>
        /// <returns>An observable that emits the change set.</returns>
        IObservable<IChangeSet<NodeGroupModel, string>> Connect(Func<NodeGroupModel, bool>? predicate = null);

        /// <summary>
        /// Watches updates for a single value matching the specified key.
        /// Call <see cref="Get(string)"/> before to be sure that the group
        /// you are about to watch has been loaded and cached.
        /// </summary>
        /// <param name="id">The id of the group to watch.</param>
        /// <returns>An observable which emits the object value.</returns>
        IObservable<NodeGroupModel> Watch(string id);

        /// <summary>
        /// Get a group for the specific id.
        /// </summary>
        /// <param name="id">The group id.</param>
        /// <returns>
        /// A <see cref="Result{T}"/> that contains the group
        /// if the operation was successful.
        /// </returns>
        ValueTask<Result<NodeGroupModel>> Get(string id);

        /// <summary>
        /// Get all groups with conditions and settings definde by the
        /// <see cref="GetNodeGroups"/> request.
        /// </summary>
        /// <param name="request">The request object.</param>
        /// <returns>
        /// A <see cref="Result{T}"/> that contains the groups
        /// if the operation was successful.
        /// </returns>
        Task<Result<List<NodeGroupModel>>> GetAll(GetNodeGroups request);

        /// <summary>
        /// Create a new <see cref="NodeGroupModel"/>.
        /// </summary>
        /// <param name="request">The request object.</param>
        /// <returns>
        /// A <see cref="Result{T}"/> that contains the created group
        /// if the operation was successful.
        /// </returns>
        Task<Result<NodeGroupModel>> Create(CreateNodeGroup request);

        /// <summary>
        /// Update an existing <see cref="NodeGroupModel"/>.
        /// </summary>
        /// <param name="request">The request object.</param>
        /// <returns>
        /// A <see cref="Result{T}"/> that contains the updated Id
        /// and Concurrency stamp if the operation was successful.
        /// </returns>
        Task<Result<NodeGroupModelUpdate>> Update(UpdateNodeGroup request);

        /// <summary>
        /// Delete an existing group.
        /// </summary>
        /// <param name="request">The reuquest object.</param>
        /// <returns>
        /// A <see cref="Result{T}"/> that indicates if the operation was successful.
        /// </returns>
        Task<Result<Unit>> Delete(DeleteNodeGroup request);
    }
}