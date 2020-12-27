namespace CBM.Core.Com
{
    /// <summary>
    /// The base class for notifications.
    /// </summary>
    public abstract class Notification
    {
        /// <summary>
        /// The notification message.
        /// </summary>
        public string Message { get; }

        /// <summary>
        /// Constructor taking the message to convey.
        /// </summary>
        /// <param name="message">The message.</param>
        protected Notification(string message) => Message = message;
    }
}
