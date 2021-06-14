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

        /// <summary>
        /// This will run all tasks using Task.Run and will return the awaiter to await on.
        /// </summary>
        public static TaskAwaiter<(T1, T2)> GetAwaiter<T1, T2>(this (Task<T1>, Task<T2>) tupple) =>
            Task.Run(async () => (
                    await tupple.Item1,
                    await tupple.Item2
            )).GetAwaiter();

        /// <summary>
        /// This will run all tasks using Task.Run and will return the awaiter to await on.
        /// </summary>
        public static TaskAwaiter<(T1, T2, T3)> GetAwaiter<T1, T2, T3>(this (Task<T1>, Task<T2>, Task<T3>) tupple) =>
            Task.Run(async () => (
                    await tupple.Item1,
                    await tupple.Item2,
                    await tupple.Item3
            )).GetAwaiter();

        /// <summary>
        /// This will ignore all exceptions of the task but will still await for it to finish.
        /// </summary>
        /// <param name="task">The task to ignore.</param>
        /// <param name="exceptionHandler">An action to execute to log the exception.</param>
        /// <returns>A continuation task that marks the completion of the previous task.</returns>
        public static Task Ignore(this Task task, Action<Exception>? exceptionHandler = null) =>
            task.ContinueWith(tsk =>
            {
                if (tsk.IsFaulted)
                {
                    exceptionHandler?.Invoke(tsk.Exception!);
                }
            });

        /// <summary>
        /// This will execute the task, will return immediately and ignore all exceptions.
        /// </summary>
        /// <param name="task">The task to forget.</param>
        /// <param name="exceptionHandler">An action to execute to log the exception.</param>
        public static async void Forget(this Task task, Action<Exception>? exceptionHandler = null)
        {
            try
            {
                await task;
            }
            catch (Exception ex)
            {
                exceptionHandler?.Invoke(ex);
            }
        }
    }
}