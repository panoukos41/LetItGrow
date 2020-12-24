namespace System.Collections.Generic
{
    public static class EnumeratorExtensions
    {
        public static IEnumerator<T> GetEnumerator<T>(this IEnumerator<T> enumerator) => enumerator;

        public static IAsyncEnumerator<T> GetEnumerator<T>(this IAsyncEnumerator<T> enumerator) => enumerator;
    }
}