using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace System.Threading.Tasks
{
    public static partial class TaskExtensions
    {
        public static TaskAwaiter GetAwaiter(this IEnumerable<Task> tasks) =>
            Task.WhenAll(tasks).GetAwaiter();

        public static TaskAwaiter<T[]> GetAwaiter<T>(this IEnumerable<Task<T>> tasks) =>
            Task.WhenAll(tasks).GetAwaiter();
    }
}