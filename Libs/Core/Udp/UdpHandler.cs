using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

using CBM.Core.Com;

namespace CBM.Core.Udp
{
    /// <summary>
    /// Handler for sending and receiving simple text messages through udp.
    /// </summary>
    public class UdpHandler
    {
        /// <summary>
        /// The port to send a message to and receive a message from.
        /// </summary>
        private const int port = 11000;

        /// <summary>
        /// Udp Client for listening to incoming messages.
        /// </summary>
        private UdpClient? inBoundUdp;
        
        /// <summary>
        /// Simple active in active state.
        /// </summary>
        private bool isListening = false;

        /// <summary>
        /// Starts listening for broad casted messages.
        /// When a message is received, the content will be published as a notification.
        /// </summary>
        public void StartListening()
        {
            try {
                StopListening();
                
                NotificationHub.PublishLogNotification("Started listening");
                isListening = true;
                
                // Setup udp listener.
                inBoundUdp = new UdpClient(port);

                // Run listening activity in another thread to avoid locking a main/ui thread.
                Task.Run(() => {
                    try { 
                        // We listen for each message in a loop.
                        while (isListening) {
                            // The excepted incoming message end point.
                            // The content will be populated with the ip from another device,
                            // when we receive a message.
                            var inBoundUdpEndpoint = new IPEndPoint(IPAddress.Any, 0);
                            
                            // Wait for a message to arrive. (this will block this thread until message received)
                            var msgb = inBoundUdp.Receive(ref inBoundUdpEndpoint);
                            
                            // Read message.
                            var msg = Encoding.UTF8.GetString(msgb);
                            
                            // Notify.
                            NotificationHub.PublishLogNotification($"Received : {msg} : From {inBoundUdpEndpoint}");
                            NotificationHub.PublishMessageReceivedNotification(msg);
                            
                            // In the next loop we wait for the next message...
                        }
                    } catch (Exception exception) {
                        NotificationHub.PublishLogNotification(exception.Message);
                    }
                });
                
            } catch (Exception exception) {
                StopListening();
                NotificationHub.PublishLogNotification(exception.Message);
            }
        }
        
        /// <summary>
        /// Stops listening to broad cast messages.
        /// </summary>
        public void StopListening()
        {
            if (inBoundUdp == null) { return; }
            
            isListening = false;
            inBoundUdp?.Close();
            inBoundUdp?.Dispose();
            inBoundUdp = null;
            
            NotificationHub.PublishMessageReceivedNotification("Stopped listening.");
        }
        
        /// <summary>
        /// Sends a broadcast message with the given content to any receiver listening.
        /// </summary>
        /// <param name="msg">The message content.</param>
        public async Task BroadcastMessage(string msg)
        {
            if (string.IsNullOrWhiteSpace(msg)) { return; }
            
            try {
                NotificationHub.PublishLogNotification($"Sending: {msg}");
                
                // Setup udp client for sending.
                using var udp = new UdpClient { EnableBroadcast = true };

                // Define the endpoint for broadcasting.
                var endpoint = new IPEndPoint(IPAddress.Broadcast, port);
                var msgb = Encoding.UTF8.GetBytes(msg);
                
                // Send the message.
                await udp.SendAsync(msgb, msgb.Length, endpoint);
                
                udp.Close();
                
            } catch (Exception exception) {
                NotificationHub.PublishLogNotification(exception.Message);
            }
        }
    }
}
