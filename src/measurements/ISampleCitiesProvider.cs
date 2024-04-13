namespace Measurements;

public interface ISampleCitiesProvider
{
    Task<List<string>> GetCities(int maxCities = -1);
}