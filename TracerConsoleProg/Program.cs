// See https://aka.ms/new-console-template for more information

using OpenTelemetry;
using OpenTelemetry.Resources;
using OpenTelemetry.Trace;
using System.Diagnostics;
using TracerConsoleProg;

using var traceProvider = Sdk.CreateTracerProviderBuilder()
    .AddSource("EmailSenderActivitySource")
    .ConfigureResource(configure =>
    {
        configure.AddService("Email.sender.app", serviceVersion: "1.0.0")
        .AddAttributes(new List<KeyValuePair<string, object>> { new("environment", "dev") });
    }).AddConsoleExporter().Build();

using var traceProvider2 = Sdk.CreateTracerProviderBuilder()
    .AddSource("EmailSenderActivitySourceToWriteFile")
    .ConfigureResource(configure =>
    {
        configure.AddService("EmailWrite.sender.app", serviceVersion: "1.0.0")
        .AddAttributes(new List<KeyValuePair<string, object>> { new("environment", "dev") });
    }).AddConsoleExporter().Build();



ActivitySource.AddActivityListener(new ActivityListener()
{
    ShouldListenTo = source => source.Name == "EmailSenderActivitySourceToWriteFile",
    ActivityStarted = activity => { Console.WriteLine("ActiviftStarted"+activity.OperationName); },
    ActivityStopped = activity => { 
        Console.WriteLine("ActiviftStop" + activity.OperationName); 
    },
});


var httpService = new HttpService();
//await httpService.MakeRequestToGoogle();

await httpService.MakeRequestToAmazon();

Console.WriteLine("Hello, World!");

Console.ReadLine();
