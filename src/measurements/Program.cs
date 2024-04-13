using System.CommandLine;
using System.CommandLine.Builder;
using System.CommandLine.Hosting;
using System.CommandLine.Invocation;
using System.CommandLine.Parsing;
using Microsoft.Extensions.Hosting;

var runner = new CommandLineBuilder(new GenerateMeasurements())
    .UseHost(_ => new HostBuilder(), builder => builder
        .ConfigureServices((_, services) => { })
        .UseCommandHandler<GenerateMeasurements, GenerateMeasurements.Handler>())
    .UseDefaults()
    .Build();

await runner.InvokeAsync(args);

public sealed class GenerateMeasurements : RootCommand
{
    public GenerateMeasurements()
        : base("Generate measurements file")
    {
        // AddArgument(new Argument<string>("url", "url"));
        // AddOption(new Option<string>(
        //     ["--output", "-o"], "Output file path"));
    }

    public new class Handler : ICommandHandler
    {
        public string? Url { get; set; }
        public string? Output { get; set; }

        public Handler()
        {
            //...
        }

        public int Invoke(InvocationContext context)
        {
            throw new NotImplementedException();
        }

        public Task<int> InvokeAsync(InvocationContext context)
        {
            return Task.FromResult(0);
        }
    }
}