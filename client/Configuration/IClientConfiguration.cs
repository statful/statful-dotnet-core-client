using Statful.Core.Client.Utils;
using Statful.Core.Client.Utils.Logging;

namespace Statful.Core.Client.Configuration
{
    public interface IClientConfiguration
    {
        string Host { get; }
        int Port { get; }
        string Transport { get; }
        bool Secure { get; }
        int Timeout { get; }
        string Token { get; }
        string App { get; }
        bool Dryrun { get; }
        ILogger Logger { get; }
        bool CacheDns { get; }
        string Tags { get; }
        int FlushInterval { get; }
        int SampleRate { get; }
        string Namespace { get; }
        string Path { get; }
    }
}