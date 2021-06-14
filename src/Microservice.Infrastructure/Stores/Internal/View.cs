using CouchDB.Driver.Types;
using System.Collections;
using System.Collections.Generic;

namespace LetItGrow.Microservice.Stores.Internal
{
    public class View<TKey, TValue, TDoc> where TDoc : CouchDocument
    {
        public string Design { get; }

        public string Value { get; }

        public View(string design, string view)
        {
            Design = design;
            Value = view;
        }
    }

    public abstract class ViewKey : IEnumerable<object>
    {
        public abstract IEnumerable<object> Key { get; }

        public IEnumerator<object> GetEnumerator() => Key.GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}