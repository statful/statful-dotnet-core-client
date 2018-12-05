namespace Statful.Core.Client.Messages
{
    public interface IMessageBuilder
    {
        IMessageBuilder WithPrefix(string prefix);
        IMessageBuilder WithNamespace(string nspace);
        IMessageBuilder WithMetricName(string metricname);
        IMessageBuilder WithValue(decimal value);
        IMessageBuilder WithTags(string tags);
        IMessageBuilder WithAggregations(string aggregations);
        IMessageBuilder WithFrequency(int frequency);
        IMessageBuilder WithSampleRate(int samplerate);
        IMessageBuilder WithApplication(string application);
        string Build();
    }
}