using CouchDB.Driver;
using CouchDB.Driver.Exceptions;
using CouchDB.Driver.Views;
using LetItGrow.Microservice.Abstraction.Stores;
using LetItGrow.Microservice.Common;
using LetItGrow.Microservice.Entities;
using LetItGrow.Microservice.Node.Models;
using LetItGrow.Microservice.Node.Requests;
using LetItGrow.Microservice.Services;
using LetItGrow.Microservice.Stores.Internal;
using Microsoft.Extensions.Configuration;
using System;
using System.Linq;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;

namespace LetItGrow.Microservice.Stores
{
    using Node = Entities.Node;

    public class NodeStore : StoreBase<Node>, INodeStore
    {
        private readonly IGroupStore groupStore;
        private readonly IEntityManager manager;

        public NodeStore(ICouchClient client, IGroupStore groupStore, IConfiguration configuration, IEntityManager manager)
            : base(client, configuration)
        {
            this.groupStore = groupStore;
            this.manager = manager;
        }

        /// <inheritdoc/>
        protected override string Discriminator { get; } = Discriminators.Node;

        /// <inheritdoc/>
        public async ValueTask<NodeModel[]> Search(SearchNodes request, CancellationToken token)
        {
            var view = request.GroupId is null
                ? Views.Node
                : Views.Node_GroupId;

            var options = new CouchViewOptions<string>
            {
                IncludeDocs = true,
                Key = request.GroupId
            };

            try
            {
                return (await GetDatabase()
                    .GetViewAsync(view, options, token)
                    .ConfigureAwait(false))
                    .Select(x => x.Document.ToModel())
                    .ToArray();
            }
            catch (Exception ex)
            {
                throw new ErrorException(Errors.ServiceUnavailable, ex);
            }
        }

        /// <inheritdoc/>
        public async ValueTask<NodeModel?> Find(string id, CancellationToken token)
        {
            try
            {
                return (await GetDatabase()
                    .FindAsync(id, cancellationToken: token)
                    .ConfigureAwait(false))
                    ?.ToModel();
            }
            catch (Exception ex)
            {
                throw new ErrorException(Errors.ServiceUnavailable, ex);
            }
        }

        /// <inheritdoc/>
        public async Task<NodeModel> CreateAsync(CreateNode request, CancellationToken token)
        {
            var node = new Node
            {
                Id = manager.CreateId(),
                Type = request.Type,
                Token = manager.CreateToken(),
                Name = request.Name,
                Description = request.Description
            };

            node.Settings = request.Type switch
            {
                NodeType.Irrigation => request.Settings.ToJObject<IrrigationSettings>(),
                NodeType.Measurement => request.Settings.ToJObject<MeasurementSettings>(),
                _ => throw new ErrorException(Errors.Validation with
                {
                    Detail = "Type must be 'Irrigation' or 'Measurement'"
                })
            };

            manager.SetAuditCreated(node.Audit);
            manager.SetAuditUpdated(node.Audit);

            try
            {
                await GetDatabase().AddAsync(node, cancellationToken: token);
            }
            catch (Exception ex)
            {
                throw new ErrorException(Errors.ServiceUnavailable, ex);
            }

            return node.ToModel();
        }

        /// <inheritdoc/>
        public async Task<NodeModel> UpdateAsync(UpdateNode request, CancellationToken token)
        {
            var db = GetDatabase();

            // Find the specified node.
            var node = await db.FindAsync(request.Id, cancellationToken: token);

            // Checks.
            node.CheckFound();
            node!.CheckConcurrency(request.ConcurrencyStamp);

            int changes = 0;
            if (request.Name is not null)
            {
                node!.Name = request.Name;
                changes++;
            }
            if (request.Description is not null)
            {
                node!.Description = request.Description;
                changes++;
            }
            if (request.Settings is not null)
            {
                switch (node!.Type)
                {
                    case NodeType.Irrigation:
                        node.Settings = request.Settings.ToJObject<IrrigationSettings>();
                        break;

                    case NodeType.Measurement:
                        node.Settings = request.Settings.ToJObject<MeasurementSettings>();
                        break;
                }
                changes++;
            }

            // Update only when needed.
            if (changes != 0)
            {
                manager.SetAuditUpdated(node!.Audit);
                try
                {
                    await db.AddOrUpdateAsync(node, cancellationToken: token);
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
            return node!.ToModel();
        }

        /// <inheritdoc/>
        public async Task DeleteAsync(DeleteNode request, CancellationToken token)
        {
            var node = new Node
            {
                Id = request.Id,
                Rev = request.ConcurrencyStamp
            };

            try
            {
                await GetDatabase().RemoveAsync(node, cancellationToken: token).ConfigureAwait(false);
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

        /// <inheritdoc/>
        public async Task<NodeModel> GroupAddAsync(GroupAdd request, CancellationToken token)
        {
            var db = GetDatabase();

            // Check if the group exists.
            if ((await groupStore.Find(request.GroupId, token).ConfigureAwait(false)) is null)
                throw new ErrorException(Errors.NotFound with
                {
                    Detail = "You have provided a 'GroupId' key that doesn't exist."
                });

            var node = await db.FindAsync(request.Id, cancellationToken: token).ConfigureAwait(false);

            node.CheckFound();
            node!.CheckConcurrency(request.ConcurrencyStamp);

            if (node!.GroupId != request.GroupId)
            {
                node.GroupId = request.GroupId;
                manager.SetAuditUpdated(node.Audit);

                try
                {
                    await db.AddOrUpdateAsync(node, cancellationToken: token);
                }
                catch (CouchConflictException ex)
                {
                    throw new ErrorException(Errors.Conflict, ex);
                }
            }

            return node.ToModel();
        }

        /// <inheritdoc/>
        public async Task<NodeModel> GroupRemoveAsyn(GroupRemove request, CancellationToken token)
        {
            var db = GetDatabase();
            var node = await db.FindAsync(request.Id, cancellationToken: token).ConfigureAwait(false);

            node.CheckFound();
            node!.CheckConcurrency(request.ConcurrencyStamp);

            node!.GroupId = null;
            manager.SetAuditUpdated(node.Audit);

            try
            {
                await db.AddOrUpdateAsync(node, cancellationToken: token);
            }
            catch (CouchConflictException ex)
            {
                throw new ErrorException(Errors.Conflict, ex);
            }

            return node.ToModel();
        }

        public async Task<NodeModel> RefreshToken(RefreshToken request, CancellationToken cancellationToken)
        {
            var db = GetDatabase();
            Node? node;
            try
            {
                node = await db.FindAsync(request.Id, cancellationToken: cancellationToken).ConfigureAwait(false);
            }
            catch (CouchException ex)
            {
                throw new ErrorException(Errors.ServiceUnavailable, ex);
            }

            node.CheckFound();
            node!.CheckConcurrency(request.ConcurrencyStamp);

            node!.Token = manager.CreateToken();

            try
            {
                await db.AddOrUpdateAsync(node, cancellationToken: cancellationToken).ConfigureAwait(false);
            }
            catch (CouchException ex)
            {
                throw new ErrorException(Errors.ServiceUnavailable, ex);
            }

            return node.ToModel();
        }
    }
}