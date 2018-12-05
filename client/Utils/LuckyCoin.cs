using System;

namespace Statful.Core.Client.Utils
{
    public interface ILuckyCoin
    {
        bool Throw(int sample);
    }

    public class LuckyCoin : ILuckyCoin
    {
        public bool Throw(int sample) {
            float dividend = 100;
            var samplerateNormalized = sample/dividend;
            var filter = new Random();
            var result = filter.NextDouble();
            if (result <= samplerateNormalized) {
                return true;
            }
            return false;
        }
    }
}