using LetItGrow.Microservice.Entities.Models;

namespace LetItGrow.Microservice.Entities
{
    public interface IEntity
    {
        public string Id { get; set; }

        public string Rev { get; set; }
    }

    public interface IAuditableEntity
    {
        public AuditInfo Audit { get; set; }
    }
}