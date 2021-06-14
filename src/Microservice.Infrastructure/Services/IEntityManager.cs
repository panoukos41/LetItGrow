using LetItGrow.Microservice.Entities.Models;
using System;

namespace LetItGrow.Microservice.Services
{
    public interface IEntityManager
    {
        void SetAuditCreated(AuditInfo info);

        void SetAuditUpdated(AuditInfo info);

        string CreateId();

        string CreateToken();

        DateTimeOffset GetNow();
    }
}