using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace LetItGrow.Services
{
    /// <summary>
    /// An interface describing the methods a db context should implement.
    /// </summary>
    public interface IAppRepository
    {
        /// <summary>
        /// Finds an entity with the given primary key values. If an entity with the given
        /// primary key values is being tracked by the context, then it is returned immediately
        /// without making a request to the database. Otherwise, a query is made to the database
        /// for an entity with the given primary key values and this entity, if found, is
        /// attached to the context and returned. If no entity is found, then null is returned.
        /// </summary>
        /// <param name="keyValues">The values of the primary key for the entity to be found.</param>
        /// <typeparam name="TEntity">The type of entity to find.</typeparam>
        /// <returns>The entity found, or null.</returns>
        ValueTask<TEntity> FindAsync<TEntity>(params object[] keyValues) where TEntity : class;

        /// <summary>
        /// Get an <see cref="IQueryable{T}"/> for the specific entity type.
        /// By default the query is not tracking the entities but performs
        /// identity resolution.<br/>
        /// More at <see href="https://docs.microsoft.com/en-us/ef/core/querying/tracking"/>
        /// </summary>
        /// <typeparam name="TEntity">The type of the entity to query.</typeparam>
        IQueryable<TEntity> Query<TEntity>() where TEntity : class;

        /// <summary>
        /// Get an <see cref="IQueryable{T}"/> for the specific entity type.
        /// The query is tracking the entities that means any changes will be tracked.
        /// More at <see href="https://docs.microsoft.com/en-us/ef/core/querying/tracking"/>
        /// </summary>
        /// <typeparam name="TEntity">The type of the entity to query.</typeparam>
        IQueryable<TEntity> QueryTracking<TEntity>() where TEntity : class;

        /// <summary>
        /// Saves all changes made in this context to the database.
        /// Use 'await' to ensure that any asynchronous operations
        /// have completed before calling another method on this context.
        /// </summary>
        /// <param name="cancellationToken">
        /// A <see cref="CancellationToken"/> to observe while waiting for the task to complete.
        /// </param>
        /// <returns>A task that represents the asynchronous save operation. The task result contains
        /// the number of state entries written to the database.</returns>
        Task<int> SaveChangesAsync(CancellationToken cancellationToken);

        /// <summary>
        /// Saves all changes made in this context to the database.
        /// </summary>
        /// <returns>The number of state entries written to the database.</returns>
        int SaveChanges();

        // todo: Summarry
        void Attach(object entity);

        // todo: Summarry
        void Attach<TEntity>(TEntity entity) where TEntity : class;

        /// <summary>
        /// Tracks the given entity, and any other reachable entities that are not already being tracked,
        /// in the Added state such that they will be inserted into the database when
        /// <see cref="SaveChanges"/> is called.
        /// </summary>
        void Add(object entity);

        /// <summary>
        /// Tracks the given entity, and any other reachable entities that are not already being tracked,
        /// in the Added state such that they will be inserted into the database when
        /// <see cref="SaveChanges"/> is called.
        /// </summary>
        void Add<TEntity>(TEntity entity) where TEntity : class;

        /// <summary>
        /// Begins tracking the given entities, and any other reachable entities that are not
        /// already being tracked, in the Added state such taht they will be inserted into the
        /// database when <see cref="SaveChanges"/> is called.
        /// </summary>
        void AddRange(params object[] entities);

        /// <summary>
        /// Begins tracking the given entities, and any other reachable entities that are not
        /// already being tracked, in the Added state such taht they will be inserted into the
        /// database when <see cref="SaveChanges"/> is called.
        /// </summary>
        void AddRange(IEnumerable<object> entities);

        /// <summary>
        /// Tracks the given entity and entries reachable from the given entity using Modified state by default.<br/>
        /// <br/>
        /// Generally, no database interaction will be performed until <see cref="SaveChanges"/> is called.<br/>
        /// <br/>
        /// A recursive search of the navigation properties will be performed to find reachable
        /// entities that are not already being tracked by the context. All entities found
        /// will be tracked by the context.<br/>
        /// <br/>
        /// For entity types with generated keys if an entity has its primary key value set
        /// then it will be tracked in the Modified state.
        /// If the primary key value is not set then it will be tracked in the Added state.
        /// This helps ensure new entities will be inserted, while existing entities
        /// will be updated. An entity is considered to have its primary key value set if
        /// the primary key property is set to anything other than the CLR default for the property type.<br/>
        /// <br/>
        /// For entity types without generated keys, the state set is always Modified.
        /// </summary>
        void Update(object entity);

        /// <summary>
        /// Tracks the given entity and entries reachable from the given entity using Modified state by default.<br/>
        /// <br/>
        /// Generally, no database interaction will be performed until <see cref="SaveChanges"/> is called.<br/>
        /// <br/>
        /// A recursive search of the navigation properties will be performed to find reachable
        /// entities that are not already being tracked by the context. All entities found
        /// will be tracked by the context.<br/>
        /// <br/>
        /// For entity types with generated keys if an entity has its primary key value set
        /// then it will be tracked in the Modified state.
        /// If the primary key value is not set then it will be tracked in the Added state.
        /// This helps ensure new entities will be inserted, while existing entities
        /// will be updated. An entity is considered to have its primary key value set if
        /// the primary key property is set to anything other than the CLR default for the property type.<br/>
        /// <br/>
        /// For entity types without generated keys, the state set is always Modified.
        /// </summary>
        void Update<TEntity>(TEntity entity) where TEntity : class;

        /// <summary>
        /// Begins tracking the given entities and entries reachable from the given entities
        /// using the Modified state by default.<br/>
        /// <br/>
        /// Generally, no database interaction will be performed until <see cref="SaveChanges"/> is called.<br/>
        /// <br/>
        /// A recursive search of the navigation properties will be performed to find reachable
        /// entities that are not already being tracked by the context. All entities found
        /// will be tracked by the context.<br/>
        /// <br/>
        /// For entity types with generated keys if an entity has its primary key value set
        /// then it will be tracked in the Modified state.
        /// If the primary key value is not set then it will be tracked in the Added state.
        /// This helps ensure new entities will be inserted, while existing entities
        /// will be updated. An entity is considered to have its primary key value set if
        /// the primary key property is set to anything other than the CLR default for the property type.<br/>
        /// <br/>
        /// For entity types without generated keys, the state set is always Modified.
        /// </summary>
        void UpdateRange(params object[] entities);

        /// <summary>
        /// Begins tracking the given entities and entries reachable from the given entities
        /// using the Modified state by default.<br/>
        /// <br/>
        /// Generally, no database interaction will be performed until <see cref="SaveChanges"/> is called.<br/>
        /// <br/>
        /// A recursive search of the navigation properties will be performed to find reachable
        /// entities that are not already being tracked by the context. All entities found
        /// will be tracked by the context.<br/>
        /// <br/>
        /// For entity types with generated keys if an entity has its primary key value set
        /// then it will be tracked in the Modified state.
        /// If the primary key value is not set then it will be tracked in the Added state.
        /// This helps ensure new entities will be inserted, while existing entities
        /// will be updated. An entity is considered to have its primary key value set if
        /// the primary key property is set to anything other than the CLR default for the property type.<br/>
        /// <br/>
        /// For entity types without generated keys, the state set is always Modified.
        /// </summary>
        void UpdateRange(IEnumerable<object> entities);

        /// <summary>
        /// Tracks the given entity in the Deleted state such that it will be removed
        /// from the database when <see cref="SaveChanges"/> is called.<br/>
        /// <br/>
        /// If the entity is already tracked in the Added state then the context will stop
        /// tracking the entity rather than marking it as Deleted since the entity was added
        /// to the context and does not exist in the database.
        /// </summary>
        void Remove(object entity);

        /// <summary>
        /// Tracks the given entity in the Deleted state such that it will be removed
        /// from the database when <see cref="SaveChanges"/> is called.<br/>
        /// <br/>
        /// If the entity is already tracked in the Added state then the context will stop
        /// tracking the entity rather than marking it as Deleted since the entity was added
        /// to the context and does not exist in the database.
        /// </summary>
        void Remove<TEntity>(TEntity entity) where TEntity : class;

        /// <summary>
        /// Tracks the given entity in the Deleted state such that it will be removed
        /// from the database when <see cref="SaveChanges"/> is called.<br/>
        /// <br/>
        /// If any of the entities are already tracked in the Added state then the context will stop
        /// tracking the entity rather than marking it as Deleted since the entity was added
        /// to the context and does not exist in the database.
        /// </summary>
        void RemoveRange(params object[] entities);

        /// <summary>
        /// Tracks the given entity in the Deleted state such that it will be removed
        /// from the database when <see cref="SaveChanges"/> is called.<br/>
        /// <br/>
        /// If any of the entities are already tracked in the Added state then the context will stop
        /// tracking the entity rather than marking it as Deleted since the entity was added
        /// to the context and does not exist in the database.
        /// </summary>
        void RemoveRange(IEnumerable<object> entities);
    }
}