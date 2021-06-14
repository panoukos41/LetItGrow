using LetItGrow.Microservice.Common;
using LetItGrow.Microservice.Entities.Models;
using LetItGrow.Services;
using System;

namespace LetItGrow.Microservice.Services
{
    public class EntityManager : IEntityManager
    {
        private readonly IClockService clock;
        private readonly IUserService userService;
        private readonly IPrimaryKeyService keyService;
        private readonly ITokenService tokenService;

        public EntityManager(IClockService clock, IUserService userService, IPrimaryKeyService keyService, ITokenService tokenService)
        {
            this.clock = clock;
            this.userService = userService;
            this.keyService = keyService;
            this.tokenService = tokenService;
        }

        /// <inheritdoc/>
        public string CreateId()
        {
            return keyService.Create();
        }

        /// <inheritdoc/>
        public string CreateToken()
        {
            return tokenService.Create();
        }

        /// <inheritdoc/>
        public DateTimeOffset GetNow()
        {
            return clock.GetNow();
        }

        /// <inheritdoc/>
        public void SetAuditUpdated(AuditInfo info)
        {
            info.UpdatedBy = userService.GetId() ?? throw new ErrorException(Errors.Unauthorized);
            info.UpdatedAt = clock.GetNow();
        }

        /// <inheritdoc/>
        public void SetAuditCreated(AuditInfo info)
        {
            info.CreatedBy = userService.GetId() ?? throw new ErrorException(Errors.Unauthorized);
            info.CreatedAt = clock.GetNow();
        }
    }
}