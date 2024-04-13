using System.IO.Compression;

namespace Measurements;

public sealed class ResourceCitiesProvider : ISampleCitiesProvider
{
    public async Task<List<string>> GetCities(int maxCities = -1)
    {
        var cities = new List<string>();
        await using Stream stationStream = typeof(ResourceCitiesProvider).Assembly.GetManifestResourceStream("measurements.Data.cities.zip")!;
        using ZipArchive zipArchive = new ZipArchive(stationStream, ZipArchiveMode.Read);

        ZipArchiveEntry entry = zipArchive.GetEntry("cities.txt")!;
        using StreamReader reader = new StreamReader(entry.Open());

        int citiesCount = 0;
        string? line = await reader.ReadLineAsync();
        while (line is not null && (maxCities < 0 || citiesCount < maxCities))
        {
            if (!line.Trim().StartsWith('#'))
            {
                cities.Add(line);
                citiesCount++;
            }

            line = await reader.ReadLineAsync();
        }

        return cities;
    }
}