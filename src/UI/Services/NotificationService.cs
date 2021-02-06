using System;
using System.Reactive.Linq;
using System.Reactive.Subjects;

namespace LetItGrow.UI.Services
{
    public class NotificationService : INotificationService
    {
        /// <summary>
        /// The underlying subject that sends and receives messages.
        /// </summary>
        protected readonly Subject<string> subject = new();

        /// <inheritdoc/>
        public void Send(string data)
        {
            subject.OnNext(data);
        }

        /// <inheritdoc/>
        public IObservable<string> Receive()
        {
            return subject.AsObservable();
        }
    }
}