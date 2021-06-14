using CouchDB.Driver.Types;
using CouchDB.Driver.Views;
using LetItGrow.Microservice.Stores.Internal;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace CouchDB.Driver
{
    internal static class CouchDBExtensions
    {
        internal static Task<List<CouchView<TKey, TValue, TDoc>>> GetViewAsync<TKey, TValue, TDoc>(
            this ICouchDatabase<TDoc> db,
            View<TKey, TValue, TDoc> view,
            CouchViewOptions<TKey>? options = null,
            CancellationToken cancellationToken = default)
            where TDoc : CouchDocument
        {
            return db.GetViewAsync<TKey, TValue>(view.Design, view.Value, options, cancellationToken);
        }

        internal static Task<List<CouchView<TKey, TValue, TDoc>>[]> GetViewQueryAsync<TKey, TValue, TDoc>(
            this ICouchDatabase<TDoc> db,
            View<TKey, TValue, TDoc> view,
            IList<CouchViewOptions<TKey>> options,
            CancellationToken cancellationToken = default)
            where TDoc : CouchDocument
        {
            return db.GetViewQueryAsync<TKey, TValue>(view.Design, view.Value, options, cancellationToken);
        }

        internal static Task<CouchViewList<TKey, TValue, TDoc>> GetDetailedViewAsync<TKey, TValue, TDoc>(
            this ICouchDatabase<TDoc> db,
            View<TKey, TValue, TDoc> view,
            CouchViewOptions<TKey>? options = null,
            CancellationToken cancellationToken = default)
            where TDoc : CouchDocument
        {
            return db.GetDetailedViewAsync<TKey, TValue>(view.Design, view.Value, options, cancellationToken);
        }

        internal static Task<CouchViewList<TKey, TValue, TDoc>[]> GetDetailedViewQueryAsync<TKey, TValue, TDoc>(
            this ICouchDatabase<TDoc> db,
            View<TKey, TValue, TDoc> view,
            IList<CouchViewOptions<TKey>> options,
            CancellationToken cancellationToken = default)
            where TDoc : CouchDocument
        {
            return db.GetDetailedViewQueryAsync<TKey, TValue>(view.Design, view.Value, options, cancellationToken);
        }
    }
}