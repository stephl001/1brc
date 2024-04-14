using System.CommandLine;
using System.CommandLine.Builder;
using System.CommandLine.Hosting;
using System.CommandLine.Invocation;
using System.CommandLine.Parsing;
using Measurements;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

var runner = new CommandLineBuilder(new GenerateMeasurements())
    .UseHost(_ => new HostBuilder(), builder => builder
        .ConfigureServices((_, services) =>
        {
            services.AddTransient<IStationLoader, StationLoader>();
            services.AddTransient<ISampleCitiesProvider, ResourceCitiesProvider>();
            services.AddTransient<IRandomTemperatureGenerator, RandomTemperatureGenerator>();
        })
        .UseCommandHandler<GenerateMeasurements, GenerateMeasurements.Handler>())
    .UseDefaults()
    .Build();

await runner.InvokeAsync(args);

public sealed class GenerateMeasurements : RootCommand
{
    public GenerateMeasurements()
        : base("Generate measurements file")
    {
        AddArgument(new Argument<int>("count", "Number of weather entries to generate"));
        AddOption(new Option<int>(
             ["--cities", "-c"], "Number of cities to pick from city list (~40000)"));
    }

    public new class Handler(IStationLoader stationLoader) : ICommandHandler
    {
        public int Count { get; set; }
        public int? Cities { get; set; }

        public int Invoke(InvocationContext context)
        {
            throw new NotImplementedException();
        }

        public async Task<int> InvokeAsync(InvocationContext context)
        {
            await using Stream outputStream = File.OpenWrite(@"c:\temp\measurements.txt");
            await using StreamWriter writer = new StreamWriter(outputStream);
            await foreach (WeatherStation ws in stationLoader.GetRandomStationsAsync(Cities ?? -1, Count))
            {
                await writer.WriteLineAsync($"{ws.City};{ws.Temperature:f2}");
            }

            return 0;
        }
    }
}