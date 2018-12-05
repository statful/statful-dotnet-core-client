using System;

namespace Statful.Core.Client.Buffer
{
    public interface IMessageBuffer : IDisposable
    {
        void Save(string message);

        void Flush();
    }
}