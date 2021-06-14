using LetItGrow.Microservice.Group.Models;
using LetItGrow.Microservice.Irrigation.Models;
using LetItGrow.Microservice.Measurement.Models;
using LetItGrow.Microservice.Node.Models;
using Newtonsoft.Json;

namespace LetItGrow.Microservice.Entities
{
    public static class EntityExtensions
    {
        public static NodeModel ToModel(this Node entity) => new()
        {
            Id = entity.Id,
            ConcurrencyStamp = entity.Rev,
            Type = entity.Type,
            Token = entity.Token,
            Name = entity.Name,
            Description = entity.Description,
            GroupId = entity.GroupId,
            Settings = entity.Settings.ToJsonDocument(),
            CreatedAt = entity.Audit.CreatedAt.ToUniversalTime(),
            CreatedBy = entity.Audit.CreatedBy,
            UpdatedAt = entity.Audit.UpdatedAt.ToUniversalTime(),
            UpdatedBy = entity.Audit.UpdatedBy,
            Connected = entity.Connected
        };

        public static GroupModel ToModel(this Group entity) => new()
        {
            Id = entity.Id,
            ConcurrencyStamp = entity.Rev,
            Type = entity.Type,
            Name = entity.Name,
            Description = entity.Description,
            CreatedAt = entity.Audit.CreatedAt.ToUniversalTime(),
            CreatedBy = entity.Audit.CreatedBy,
            UpdatedAt = entity.Audit.UpdatedAt.ToUniversalTime(),
            UpdatedBy = entity.Audit.UpdatedBy
        };

        public static IrrigationModel ToModel(this Irrigation entity) => new()
        {
            Id = entity.Id,
            NodeId = entity.NodeId,
            Type = entity.Type,
            IssuedAt = entity.IssuedAt.ToUniversalTime(),
            CreatedAt = entity.CreatedAt.ToUniversalTime()
        };

        public static MeasurementModel ToModel(this Measurement entity) => new()
        {
            Id = entity.Id,
            NodeId = entity.NodeId,
            AirHumidity = entity.AirHumidity,
            AirTemperatureC = entity.AirTemperatureC,
            SoilMoisture = entity.SoilMoisture,
            CreatedAt = entity.CreatedAt.ToUniversalTime(),
            MeasuredAt = entity.MeasuredAt.ToUniversalTime(),
        };
    }
}