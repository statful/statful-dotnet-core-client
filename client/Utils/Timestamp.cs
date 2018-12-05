using System;

namespace Statful.Core.Client.Utils
{
    public interface ITimestamp
    {
        int Now();
    }

    public class Timestamp : ITimestamp
    {
        public int Now() {
            return (int) (DateTime.UtcNow.Subtract(new DateTime(1970, 1, 1))).TotalSeconds;
        }
    }
}