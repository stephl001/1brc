// See https://aka.ms/new-console-template for more information
var allStations = ListAllStations().ToList();
Console.WriteLine($"There are {allStations.Count} stations loaded.");

IEnumerable<WeatherStation> ListAllStations()
{
    using Stream stationStream = typeof(Program).Assembly.GetManifestResourceStream("measurements.Data.weather_stations.csv")!;
    using StreamReader reader = new StreamReader(stationStream);
    string? line = reader.ReadLine();
    while (line is not null)
    {
        if (!line.StartsWith('#'))
        {
            string[] lineParts = line.Split(';');
            yield return new WeatherStation(lineParts[0], double.Parse(lineParts[1]));
        }

        line = reader.ReadLine();
    }
}

public record WeatherStation(string City, double Temperature);