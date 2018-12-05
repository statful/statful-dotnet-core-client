using System;

namespace Statful.Core.Client.Client
{
    public interface IStatfulClient : IDisposable
    {
        void Inc(string name, decimal value, string tags, string aggregations, int frequency = 10, int samplerate = 100);

        void Time(string name, decimal value, string tags, string aggregations, int frequency = 10, int samplerate = 100);

        void Gauge(string name, decimal value, string tags, string aggregations, int frequency = 10, int samplerate = 100);

        void Put(string metric, string tags, decimal value, string aggregations, int frequency = 10, int samplerate = 1);
    }
}