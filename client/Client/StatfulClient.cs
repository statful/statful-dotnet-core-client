using System;
using System.Runtime.CompilerServices;
using Statful.Core.Client.Buffer;
using Statful.Core.Client.Configuration;
using Statful.Core.Client.Messages;
using Statful.Core.Client.Utils;
using Statful.Core.Client.Utils.Logging;

[assembly: InternalsVisibleTo("Statful.Core.Client.UnitTests.Client")]
namespace Statful.Core.Client.Client
{
    public class StatfulClient : IStatfulClient
    {
        private static readonly object sharedstatelock = new object();
        private static bool isDisposed;

        private readonly IMessageBuffer buffer;
        private readonly IClientConfiguration configurationGateway;
        private readonly ILogger logger;
        private readonly ILuckyCoin luckyCoin;
        private readonly ITimestamp timestamp;

        public StatfulClient(IClientConfiguration configurationGateway, IMessageBuffer buffer, ITimestamp timestamp,
            ILuckyCoin luckyCoin)
        {
            this.buffer = buffer ?? throw new ArgumentNullException("buffer", "Buffer message cannot be null!");
            this.timestamp = timestamp ?? throw new ArgumentNullException("timestamp", "Timestamp calculator cannot be null!");
            this.luckyCoin = luckyCoin ?? throw new ArgumentNullException("luckyCoin", "Lucky coin cannot be null!");

            this.configurationGateway = configurationGateway ?? throw new ArgumentNullException("configurationGateway", "Configuration gateway cannot be null!");
            this.logger = configurationGateway.Logger;

        }

        public void Inc(string name, decimal value, string tags, string aggregations, int frequency = 10, int samplerate = 100)
        {
            aggregations = string.IsNullOrWhiteSpace(aggregations) ? "sum,count" : aggregations;
            this.Put("counter." + name, tags, value, aggregations, frequency, samplerate);
        }

        public void Time(string name, decimal value, string tags, string aggregations, int frequency = 10, int samplerate = 100)
        {
            aggregations = string.IsNullOrWhiteSpace(aggregations) ? "avg,p90,count" : aggregations;
            this.Put("timer." + name, tags, value, aggregations, frequency, samplerate);
        }

        public void Gauge(string name, decimal value, string tags, string aggregations, int frequency = 10, int samplerate = 100)
        {
            aggregations = string.IsNullOrWhiteSpace(aggregations) ? "last" : aggregations;
            this.Put("gauge." + name, tags, value, aggregations, frequency, samplerate);
        }

        public void Put(string metric, string tags, decimal value, string aggregations, int frequency = 10, int samplerate = 1)
        {
            if (metric == null)
            {
                metric = string.Empty;
            }
            if (tags == null)
            {
                tags = configurationGateway.Tags;
            }
            else
            {
                tags = configurationGateway.Tags + "," + tags;
            }
            if (aggregations == null)
            {
                aggregations = string.Empty;
            }

            this.PutMessage(metric, tags, value, aggregations, frequency, samplerate);
        }

        public void Dispose()
        {
            lock (sharedstatelock)
            {
                this.Dispose(true);
                GC.SuppressFinalize(this);
            }
        }

        private void PutMessage(string metric, string tags, decimal value, string aggregations, int frequency, int samplerate)
        {
            try
            {
                if (this.ShouldProcessMessage(samplerate))
                {
                    var message = this.BuildMessage(metric, tags, value, aggregations, frequency, samplerate);
                    this.buffer.Save(message);
                }
            }
            catch (Exception ex)
            {
                this.logger.ErrorFormat("An error occur while processing a Statful metric {0}!", metric);
                this.logger.ErrorFormat("Error message: {0}", ex.Message);
            }
        }

        private string BuildMessage(string metric, string tags, decimal value, string aggregations, int frequency, int samplerate)
        {
            var messageBuilder = new MessageBuilder(this.timestamp);
            return messageBuilder.WithNamespace(this.configurationGateway.Namespace)
                .WithMetricName(metric)
                .WithTags(tags)
                .WithValue(value)
                .WithAggregations(aggregations)
                .WithFrequency(frequency)
                .WithSampleRate(samplerate)
                .WithApplication(this.configurationGateway.App)
                .Build();
        }

        private bool ShouldProcessMessage(int samplerate)
        {
            return this.luckyCoin.Throw(samplerate);
        }

        private void Dispose(bool disposing)
        {
            if (isDisposed)
            {
                return;
            }
            if (disposing)
            {
                if (this.buffer != null)
                {
                    this.buffer.Dispose();
                }
            }

            isDisposed = true;
        }

        ~StatfulClient()
        {
            this.Dispose(false);
        }
    }
}