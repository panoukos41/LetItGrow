using CouchDB.Driver;
using CouchDB.Driver.Exceptions;
using CouchDB.Driver.Views;
using LetItGrow.Microservice.Abstraction.Stores;
using LetItGrow.Microservice.Common;
using LetItGrow.Microservice.Entities;
using LetItGrow.Microservice.Irrigation.Models;
using LetItGrow.Microservice.Irrigation.Requests;
using LetItGrow.Microservice.Services;
using LetItGrow.Microservice.Stores.Internal;
using Microsoft.Extensions.Configuration;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace LetItGrow.Microservice.Stores
{
    public class IrrigationStore : StoreBase<Entities.Irrigation>, IIrrigationStore
    {
        private readonly IEntityManager entity;

        public IrrigationStore(IEntityManager entity, ICouchClient client, IConfiguration configuration)
            : base(client, configuration)
        {
            this.entity = entity;
        }

        /// <inheritdoc/>
        protected override string Discriminator { get; } = Discriminators.Irrigation;

        /// <inheritdoc/>
        public async ValueTask<IrrigationModel[]> Search(SearchIrrigations request, CancellationToken cancellationToken)
        {
            var options = new CouchViewOptions<Views.IrrigationViewKey>
            {
                IncludeDocs = true,
                Descending = true,
                StartKey = new(request.NodeId, request.StartDate),
                EndKey = new(request.NodeId, request.EndDate)
            };

            return (await GetDatabase()
                .GetViewAsync(Views.Irrigations, options, cancellationToken)
                .ConfigureAwait(false))
                .Select(x => x.Document.ToModel())
                .ToArray();
        }

        /// <inheritdoc/>
        public async ValueTask<IrrigationModel[]> SearchMany(SearchIrrigations[] requests, CancellationToken cancellationToken)
        {
            var options = requests
                .Select(request =>
                    new CouchViewOptions<Views.IrrigationViewKey>
                    {
                        IncludeDocs = true,
                        Descending = true,
                        StartKey = new(request.NodeId, request.StartDate),
                        EndKey = new(request.NodeId, request.EndDate)
                    })
                .ToArray();

            try
            {
                return (await GetDatabase()
                    .GetViewQueryAsync(Views.Irrigations, options, cancellationToken)
                    .ConfigureAwait(false))
                    .SelectMany(x => x.Select(e => e.Document.ToModel()))
                    .ToArray();
            }
            catch (CouchException ex)
            {
                throw new ErrorException(Errors.ServiceUnavailable, ex);
            }
        }

        /// <inheritdoc/>
        public async Task<IrrigationModel> Create(CreateIrrigation request, CancellationToken cancellationToken)
        {
            var irrigation = new Entities.Irrigation
            {
                Id = Guid.NewGuid().ToString(),
                NodeId = request.NodeId,
                IssuedAt = request.IssuedAt,
                Type = request.Type,
                CreatedAt = entity.GetNow()
            };

            try
            {
                await GetDatabase().AddAsync(irrigation, cancellationToken: cancellationToken);
            }
            catch (CouchConflictException ex)
            {
                throw new ErrorException(Errors.Conflict, ex);
            }

            return irrigation.ToModel();
        }
    }
}