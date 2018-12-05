using System;
using System.Net.Sockets;
using System.Text;
using Statful.Core.Client.Configuration;
using Statful.Core.Client.Utils;
using Statful.Core.Client.Utils.Logging;

namespace Statful.Core.Client.Server
{
    public class UdpStatfulGateway : IStatfulGateway
    {
        private readonly string host;
        private readonly ILogger logger;
        private readonly int port;
        private bool isDisposed;

        public UdpStatfulGateway(IClientConfiguration config) {
            this.host = config.Host;
            this.port = config.Port;
            this.logger = config.Logger;
        }

        public void SendAsync(string message) {
            using (var client = new UdpClient(this.host, this.port)) {
                this.logger.InfoFormat("sending to {0}:{1} at {2}: {3}", this.host, this.port, DateTime.Now.ToLongTimeString(), message);
                var bytesMessage = Encoding.UTF8.GetBytes(message);
                client.SendAsync(bytesMessage, bytesMessage.Length);
            }
        }

        public void Dispose() {
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }

        private void Dispose(bool disposing) {
            if (this.isDisposed) {
                return;
            }
            if (disposing) {}

            this.isDisposed = true;
        }

        ~UdpStatfulGateway() {
            this.Dispose(false);
        }
    }
}