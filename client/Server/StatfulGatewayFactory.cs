using System;
using Statful.Core.Client.Configuration;

namespace Statful.Core.Client.Server
{
    internal class StatfulGatewayFactory
    {
        public IStatfulGateway CreateStatfulGateway(IClientConfiguration config) {
            IStatfulGateway gateway;
            switch (config.Transport) {
                case "udp":
                    gateway = new UdpStatfulGateway(config);
                    break;
                case "http":
                    gateway = new HttpStatfulGateway(config);
                    break;
                case "tcp":
                    throw new NotImplementedException("TCP connection is not yet supported!");
                default:
                    throw new InvalidOperationException(string.Format("Unknown transport type: {0}.", config.Transport));
            }
            return gateway;
        }
    }
}