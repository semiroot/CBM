using System;

using CBM.Core.Com.Notifications;

namespace CBM.Core.Com
{
    /// <summary>
    /// The notification hub is a simple entry point to send and receive messages.
    /// </summary>
    public static class NotificationHub
    {
        /// <summary>
        /// Event containing the notification received.
        /// </summary>
        public static event EventHandler<Notification>? NotificationPublished;

        /// <summary>
        /// Publishes a new notification containing the content of the received message.
        /// </summary>
        /// <param name="message">The content of the message received.</param>
        public static void PublishMessageReceivedNotification(string message) 
            => PublishNotification(new MessageReceivedNotification(message));

        /// <summary>
        /// Publishes a new notification for logging a message.
        /// </summary>
        /// <param name="message">The log message.</param>
        public static void PublishLogNotification(string message) 
            => PublishNotification(new LogNotification(message));

        /// <summary>
        /// Publishes the given notification.
        /// </summary>
        /// <param name="n">The notification.</param>
        private static void PublishNotification(Notification n) 
            => NotificationPublished?.Invoke(null, n);
    }
}
