namespace Measurements;

public sealed class StationLoader(ISampleCitiesProvider citiesProvider, IRandomTemperatureGenerator temperatureGenerator) : IStationLoader
{
    public async IAsyncEnumerable<WeatherStation> GetRandomStationsAsync(int maxStations = -1, int maxElements = -1)
    {
        int yieldedElements = 0;
        var allCities = await citiesProvider.GetCities(maxStations);
        var indexes = Enumerable.Range(0, allCities.Count).ToArray();
        while (maxElements < 0 || yieldedElements < maxElements)
        {
            Random.Shared.Shuffle(indexes);
            for (int i = 0; i < allCities.Count; i++)
            {
                double randomTemperature = temperatureGenerator.GetTemperature();
                WeatherStation s = new WeatherStation(allCities[indexes[i]], randomTemperature);
                yield return s;
                yieldedElements++;
            }
        }
    }
}