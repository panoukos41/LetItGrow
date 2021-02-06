using System;

namespace LetItGrow.UI.Services
{
    public interface INotificationService
    {
        /// <summary>
        /// Send a message.
        /// </summary>
        /// <param name="message"></param>
        void Send(string message);

        /// <summary>
        /// Get an observable that receives messages.
        /// </summary>
        /// <returns>An observable that receives messages.</returns>
        IObservable<string> Receive();
    }
}