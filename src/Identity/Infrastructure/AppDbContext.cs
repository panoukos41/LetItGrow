using LetItGrow.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace LetItGrow.Identity.Infrastructure
{
    public class AppDbContext : IdentityDbContext, IAppRepository
    {
        // todo: In the future find a way to modify the schema without weind ways like this.
        public static string? Schema { get; set; }

        public static string? Connecion { get; set; }

        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.HasDefaultSchema(Schema);

            builder.Entity<IdentityUser>().ToTable("Users");
            builder.Entity<IdentityUserRole<string>>().ToTable("UserRoles");
            builder.Entity<IdentityUserClaim<string>>().ToTable("UserClaims");
            builder.Entity<IdentityUserLogin<string>>().ToTable("UserLogins");
            builder.Entity<IdentityUserToken<string>>().ToTable("UserTokens");

            builder.Entity<IdentityRole>().ToTable("Roles");
            builder.Entity<IdentityRoleClaim<string>>().ToTable("RoleClaims");
        }

        #region IAppDbContext

        ValueTask<TEntity> IAppRepository.FindAsync<TEntity>(params object[] keyValues) =>
            FindAsync<TEntity>(keyValues);

        IQueryable<TEntity> IAppRepository.Query<TEntity>() =>
            Set<TEntity>();

        IQueryable<TEntity> IAppRepository.QueryTracking<TEntity>() =>
            Set<TEntity>().AsTracking();

        void IAppRepository.Attach(object entity) =>
            Attach(entity);

        void IAppRepository.Attach<TEntity>(TEntity entity) =>
            Attach(entity);

        void IAppRepository.Add(object entity) =>
            Add(entity);

        void IAppRepository.Add<TEntity>(TEntity entity) =>
            Add(entity);

        void IAppRepository.Update(object entity) =>
            Update(entity);

        void IAppRepository.Update<TEntity>(TEntity entity) =>
            Update(entity);

        void IAppRepository.Remove(object entity) =>
            Remove(entity);

        void IAppRepository.Remove<TEntity>(TEntity entity) =>
            Remove(entity);

        #endregion
    }
}