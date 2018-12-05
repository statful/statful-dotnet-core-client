using System;

namespace Statful.Core.Client.Server
{
    public interface IStatfulGateway : IDisposable
    {
        void SendAsync(string message);
    }
}