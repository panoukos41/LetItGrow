using CouchDB.Driver;
using CouchDB.Driver.Exceptions;
using LetItGrow.Microservice.Abstraction.Stores;
using LetItGrow.Microservice.Common;
using LetItGrow.Microservice.Entities;
using LetItGrow.Microservice.Group.Models;
using LetItGrow.Microservice.Group.Requests;
using LetItGrow.Microservice.Services;
using LetItGrow.Microservice.Stores.Internal;
using Microsoft.Extensions.Configuration;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace LetItGrow.Microservice.Stores
{
    using Group = Entities.Group;

    public class GroupStore : StoreBase<Group>, IGroupStore
    {
        private readonly IEntityManager manager;

        public GroupStore(ICouchClient client, IConfiguration configuration, IEntityManager manager)
            : base(client, configuration)
        {
            this.manager = manager;
        }

        /// <inheritdoc/>
        protected override string Discriminator { get; } = Discriminators.Group;

        /// <inheritdoc/>
        public async ValueTask<GroupModel[]> Search(SearchGroups request, CancellationToken token)
        {
            try
            {
                return (await GetDatabase()
                    .GetViewAsync(Views.Group, new() { IncludeDocs = true }, token)
                    .ConfigureAwait(false))
                    .Select(x => x.Document.ToModel())
                    .ToArray();
            }
            catch (CouchException ex)
            {
                throw new ErrorException(Errors.ServiceUnavailable, ex);
            }
        }

        /// <inheritdoc/>
        public async ValueTask<GroupModel?> Find(string id, CancellationToken token)
        {
            try
            {
                return (await GetDatabase()
                    .FindAsync(id, cancellationToken: token)
                    .ConfigureAwait(false))
                    ?.ToModel();
            }
            catch (CouchException ex)
            {
                throw new ErrorException(Errors.ServiceUnavailable, ex);
            }
        }

        /// <inheritdoc/>
        public async Task<GroupModel> CreateAsync(CreateGroup request, CancellationToken token)
        {
            var group = new Group
            {
                Id = manager.CreateId(),
                Name = request.Name,
                Description = request.Description
            };
            manager.SetAuditCreated(group.Audit);
            manager.SetAuditUpdated(group.Audit);

            try
            {
                await GetDatabase().AddAsync(group, cancellationToken: token);
            }
            catch (CouchException ex)
            {
                throw new ErrorException(Errors.ServiceUnavailable, ex);
            }

            return group.ToModel();
        }

        /// <inheritdoc/>
        public async Task<GroupModel> UpdateAsync(UpdateGroup request, CancellationToken token)
        {
            var db = GetDatabase();
            Group? group;
            try
            {
                group = await db.FindAsync(request.Id, cancellationToken: token);
            }
            catch (CouchException ex)
            {
                throw new ErrorException(Errors.ServiceUnavailable, ex);
            }

            // Checks.
            group.CheckConcurrency(request.ConcurrencyStamp);

            int changes = 0;
            if (request.Name is not null)
            {
                group!.Name = request.Name;
                changes++;
            }
            if (request.Type is not null)
            {
                group!.Type = (GroupType)request.Type;
                changes++;
            }
            if (request.Description is not null)
            {
                group!.Description = request.Description;
                changes++;
            }

            // Update only when needed.
            if (changes != 0)
            {
                manager.SetAuditUpdated(group!.Audit);
                try
                {
                    await db.AddOrUpdateAsync(group, cancellationToken: token);
                }
                catch (CouchConflictException ex)
                {
                    throw new ErrorException(Errors.Conflict, ex);
                }
            }
            return group!.ToModel();
        }

        /// <inheritdoc/>
        public async Task DeleteAsync(DeleteGroup request, CancellationToken token)
        {
            var group = new Group
            {
                Id = request.Id,
                Rev = request.ConcurrencyStamp
            };

            try
            {
                await GetDatabase().RemoveAsync(group, cancellationToken: token).ConfigureAwait(false);
            }
            catch (CouchNotFoundException ex)
            {
                throw new ErrorException(Errors.NotFound, ex);
            }
            catch (CouchConflictException ex)
            {
                throw new ErrorException(Errors.Conflict, ex);
            }
            catch (Exception ex)
            {
                throw new ErrorException(Errors.ServiceUnavailable, ex);
            }
        }
    }
}