namespace Measurements;

public interface IStationLoader
{
    IAsyncEnumerable<WeatherStation> GetRandomStationsAsync(int maxStations = -1, int maxElements = -1);
}