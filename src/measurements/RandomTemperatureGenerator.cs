namespace Measurements;

public sealed class RandomTemperatureGenerator : IRandomTemperatureGenerator
{
    public double GetTemperature() => Random.Shared.NextDouble() * 50.0 - 10.0;
}