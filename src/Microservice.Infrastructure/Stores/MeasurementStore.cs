using CouchDB.Driver;
using CouchDB.Driver.Exceptions;
using CouchDB.Driver.Views;
using LetItGrow.Microservice.Abstraction.Stores;
using LetItGrow.Microservice.Common;
using LetItGrow.Microservice.Entities;
using LetItGrow.Microservice.Measurement.Models;
using LetItGrow.Microservice.Measurement.Requests;
using LetItGrow.Microservice.Services;
using LetItGrow.Microservice.Stores.Internal;
using Microsoft.Extensions.Configuration;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace LetItGrow.Microservice.Stores
{
    public class MeasurementStore : StoreBase<Entities.Measurement>, IMeasurementStore
    {
        private readonly IEntityManager entity;

        public MeasurementStore(IEntityManager entity, ICouchClient client, IConfiguration configuration)
            : base(client, configuration)
        {
            this.entity = entity;
        }

        /// <inheritdoc/>
        protected override string Discriminator { get; } = Discriminators.Measurement;

        /// <inheritdoc/>
        public async ValueTask<MeasurementModel[]> Search(SearchMeasurements request, CancellationToken cancellationToken)
        {
            var options = new CouchViewOptions<Views.MeasurementViewKey>
            {
                IncludeDocs = true,
                Descending = true,
                StartKey = new(request.NodeId, request.StartDate),
                EndKey = new(request.NodeId, request.EndDate)
            };

            return (await GetDatabase()
                .GetViewAsync(Views.Measurements, options, cancellationToken)
                .ConfigureAwait(false))
                .Select(x => x.Document.ToModel())
                .ToArray();
        }

        /// <inheritdoc/>
        public async ValueTask<MeasurementModel[]> SearchMany(SearchMeasurements[] requests, CancellationToken cancellationToken)
        {
            var options = requests
                .Select(request =>
                    new CouchViewOptions<Views.MeasurementViewKey>
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
                    .GetViewQueryAsync(Views.Measurements, options, cancellationToken)
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
        public async Task<MeasurementModel> Create(CreateMeasurement request, CancellationToken cancellationToken)
        {
            var measurement = new Entities.Measurement
            {
                Id = Guid.NewGuid().ToString(),
                NodeId = request.NodeId,
                MeasuredAt = request.MeasuredAt,
                CreatedAt = entity.GetNow(),
                AirTemperatureC = request.AirTemperatureC,
                AirHumidity = request.AirHumidity,
                SoilMoisture = request.SoilMoisture
            };

            try
            {
                await GetDatabase().AddAsync(measurement, cancellationToken: cancellationToken);
            }
            catch (CouchConflictException ex)
            {
                throw new ErrorException(Errors.Conflict, ex);
            }

            return measurement.ToModel();
        }
    }
}