namespace CBM.Core.Com.Notifications
{
    /// <summary>
    /// A notification for logging.
    /// </summary>
    public class LogNotification : Notification
    {
        /// <inheritdoc/>
        public LogNotification(string message) : base(message) { }
    }
}
