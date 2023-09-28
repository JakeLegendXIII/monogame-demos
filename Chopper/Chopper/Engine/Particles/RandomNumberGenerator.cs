using System;

namespace Chopper.Engine.Particles
{
    public class RandomNumberGenerator
    {
        private Random _random;

        public RandomNumberGenerator()
        {
            _random = new Random();
        }

        public int NextRandom() => _random.Next();
        public int NextRandom(int maxValue) => _random.Next(maxValue);
        public int NextRandom(int minValue, int maxValue) => _random.Next(minValue, maxValue);

        public float NextRandom(float maxValue) => (float)_random.NextDouble() * maxValue;
        public float NextRandom(float minValue, float maxValue) => (float)_random.NextDouble() * (maxValue - minValue) + minValue;
    }
}
