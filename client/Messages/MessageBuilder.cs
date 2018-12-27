using System.Text;
using Statful.Core.Client.Utils;

namespace Statful.Core.Client.Messages
{
    public class MessageBuilder : IMessageBuilder
    {
        private readonly ITimestamp timestamp;
        private string aggregations = string.Empty;
        private string application = string.Empty;
        private int frequency = 10;
        private string name = string.Empty;
        private string nspace = string.Empty;
        private int samplerate = 100;
        private string tags = string.Empty;
        private decimal value;

        public MessageBuilder(ITimestamp timestamp) {
            this.timestamp = timestamp;
        }

        public IMessageBuilder WithNamespace(string nspace) {
            if (nspace != null) {
                this.nspace = nspace;
            }
            return this;
        }

        public IMessageBuilder WithMetricName(string metricname) {
            if (metricname != null) {
                this.name = metricname;
            }
            return this;
        }

        public IMessageBuilder WithValue(decimal value) {
            this.value = value;
            return this;
        }

        public IMessageBuilder WithTags(string tags) {
            if (tags != null) {
                this.tags = tags;
            }
            return this;
        }

        public IMessageBuilder WithAggregations(string aggregations) {
            if (aggregations != null) {
                this.aggregations = aggregations;
            }
            return this;
        }

        public IMessageBuilder WithFrequency(int frequency) {
            this.frequency = frequency;
            return this;
        }

        public IMessageBuilder WithSampleRate(int samplerate) {
            if (this.samplerate >= 0 && samplerate <= 100) {
                this.samplerate = samplerate;
            }
            return this;
        }

        public IMessageBuilder WithApplication(string application) {
            if (application != null) {
                this.application = application;
            }
            return this;
        }

        public string Build() {
            var namespaceBlock = this.BuildMetricName();
            var tagsBlock = this.BuildTagsBlock();
            var aggregationsBlock = this.BuildAggregationsBlock();
            var timestamp = this.timestamp.Now();
            return string.Format("{0},{1} {2} {3} {4}", namespaceBlock, tagsBlock, this.value, timestamp, aggregationsBlock).Trim();
        }

        private string BuildMetricName() {
            return string.Format("{0}.{1}", this.nspace, this.name);
        }

        private string BuildAggregationsBlock() {
            if (string.IsNullOrWhiteSpace(this.aggregations)) {
                return string.Empty;
            }

            var samplerateBlock = (this.samplerate != 100 ? this.samplerate.ToString() : string.Empty);
            return string.Format("{0},{1} {2}", this.aggregations, this.frequency, samplerateBlock);
        }

        private string BuildTagsBlock() {
            var builder = new StringBuilder();
            builder.AppendFormat("app={0}", this.application);
            if (string.IsNullOrWhiteSpace(this.tags) == false) {
                builder.AppendFormat(",{0}", this.tags);
            }
            return builder.ToString();
        }
    }
}