namespace CBM.Core.Com.Notifications
{
    /// <summary>
    /// A notification containing a message received from another device.
    /// </summary>
    public class MessageReceivedNotification : Notification
    {
        /// <inheritdoc/>
        public MessageReceivedNotification(string message) : base(message) { }
    }
}
