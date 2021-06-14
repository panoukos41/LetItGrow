using DynamicData;
using LetItGrow.Microservice.Common;
using LetItGrow.Microservice.Common.Models;
using LetItGrow.Microservice.Group.Models;
using LetItGrow.Microservice.Group.Requests;
using MediatR;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LetItGrow.UI.Group.Services
{
    public interface IGroupService
    {
        /// <summary>
        /// Enumerate all the cached nodes.
        /// </summary>
        IEnumerable<GroupModel> Cache { get; }

        /// <summary>
        /// Returns a filtered stream of cache changes preceded with the initial filtered state.
        /// </summary>
        /// <param name="predicate">The result will be filtered using the specified predicate.</param>
        /// <returns>An observable that emits the change set.</returns>
        IObservable<IChangeSet<GroupModel, string>> Connect(Func<GroupModel, bool>? predicate = null);

        /// <summary>
        /// Watches updates for a single value matching the specified key.
        /// Call <see cref="Get(string)"/> before to be sure that the group
        /// you are about to watch has been loaded and cached.
        /// </summary>
        /// <param name="id">The id of the group to watch.</param>
        /// <returns>An observable which emits the object value.</returns>
        IObservable<GroupModel> Watch(string id);

        /// <summary>
        /// Get a group for the specific id.
        /// </summary>
        /// <param name="id">The group id.</param>
        /// <returns>
        /// A <see cref="Result{T}"/> that contains the group
        /// if the operation was successful.
        /// </returns>
        ValueTask<Result<GroupModel>> Get(string id);

        /// <summary>
        /// Get all groups with conditions and settings definde by the
        /// <see cref="SearchGroups"/> request.
        /// </summary>
        /// <param name="request">The request object.</param>
        /// <returns>
        /// A <see cref="Result{T}"/> that contains the groups
        /// if the operation was successful.
        /// </returns>
        Task<Result<GroupModel[]>> Search(SearchGroups request);

        /// <summary>
        /// Create a new <see cref="GroupModel"/>.
        /// </summary>
        /// <param name="request">The request object.</param>
        /// <returns>
        /// A <see cref="Result{T}"/> that contains the created group
        /// if the operation was successful.
        /// </returns>
        Task<Result<GroupModel>> Create(CreateGroup request);

        /// <summary>
        /// Update an existing <see cref="GroupModel"/>.
        /// </summary>
        /// <param name="request">The request object.</param>
        /// <returns>
        /// A <see cref="Result{T}"/> that contains the updated Id
        /// and Concurrency stamp if the operation was successful.
        /// </returns>
        Task<Result<ModelUpdate>> Update(UpdateGroup request);

        /// <summary>
        /// Delete an existing group.
        /// </summary>
        /// <param name="request">The reuquest object.</param>
        /// <returns>
        /// A <see cref="Result{T}"/> that indicates if the operation was successful.
        /// </returns>
        Task<Result<Unit>> Delete(DeleteGroup request);
    }
}