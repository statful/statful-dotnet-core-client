using System;
using Statful.Core.Client.Buffer;
using Statful.Core.Client.Client;
using Statful.Core.Client.Configuration;
using Statful.Core.Client.Server;
using Statful.Core.Client.Utils;

namespace Statful.Core.Client
{
    public static class StatfulClientFactory
    {
        public static IStatfulClient CreateStatfulClient(string path, string filename)
        {
            var config = new ClientConfigurationFileReader(path, filename);
            return CreateStatfulClient(config);
        }

        public static IStatfulClient CreateStatfulClient(IClientConfiguration config)
        {
            if (config == null)
            {
                throw new ArgumentNullException("config", "Configuration cannot be null!");
            }

            var telemetronGatewayFactory = new StatfulGatewayFactory();
            var telemetronGateway = telemetronGatewayFactory.CreateStatfulGateway(config);
            var messageBuffer = new MessageBuffer(telemetronGateway, config.Logger, config.FlushInterval);
            var luckyCoin = new LuckyCoin();
            var timestamp = new Timestamp();
            return new StatfulClient(config, messageBuffer, timestamp, luckyCoin);
        }
    }
}