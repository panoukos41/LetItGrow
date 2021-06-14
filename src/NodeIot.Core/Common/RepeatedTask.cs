using System;
using System.Reactive.Concurrency;
using System.Reactive.Linq;
using System.Reactive.Threading.Tasks;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;

namespace LetItGrow.NodeIot.Common
{
    /// <summary>
    /// A task that repeats all the time.
    /// Await this object to start the repeats or call Start.
    /// </summary>
    /// <remarks>
    /// If the task has not finished running it won't be executed again
    /// until it's finished. In the next interval it will be executed again.
    /// </remarks>
    public class RepeatedTask
    {
        private readonly CancellationToken _stoppingToken;
        private bool _running;
        private Task? _runningTask;

        /// <summary>
        /// Initialize a new <see cref="RepeatedTask"/> object.
        /// </summary>
        /// <param name="task">The task that will be repeated.</param>
        /// <param name="period">The time between each repeat.</param>
        /// <param name="stoppingToken">A token marking the stoppage of the tasks.</param>
        public RepeatedTask(Func<CancellationToken, Task> task, TimeSpan period, CancellationToken stoppingToken)
        {
            Task = task;
            Period = period;
            Time = Observable.Timer(TimeSpan.Zero, period, ThreadPoolScheduler.Instance);
            _stoppingToken = stoppingToken;
        }

        /// <summary>
        /// The task that is being repeated every <see cref="Period"/>.
        /// </summary>
        public Func<CancellationToken, Task> Task { get; }

        /// <summary>
        /// The amount of time between each repeat in milliseconds.
        /// </summary>
        public TimeSpan Period { get; }

        /// <summary>
        /// An Observable that ticks every <see cref="Period"/>.
        /// </summary>
        public IObservable<long> Time { get; }

        public Task Start()
        {
            return _runningTask ??= GetTask();

            Task GetTask()
            {
                if (_stoppingToken.IsCancellationRequested)
                    return System.Threading.Tasks.Task.CompletedTask;

                Time.ObserveOn(ThreadPoolScheduler.Instance)
                    .Subscribe(
                    token: _stoppingToken,
                    onNext: async next =>
                    {
                        if (_running) return;

                        _running = true;
                        await Task.Invoke(_stoppingToken).ConfigureAwait(false);
                        _running = false;
                    });

                return Time.ToTask(_stoppingToken, ThreadPoolScheduler.Instance);
            }
        }

        /// <summary>
        ///
        /// </summary>
        /// <returns></returns>
        public TaskAwaiter GetAwaiter() => Start().GetAwaiter();
    }
}