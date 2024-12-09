using Cocona;
using CSVConsoleExplorer;
using CSVConsoleExplorer.Handlers;
using CSVConsoleExplorer.Interfaces;
using CSVConsoleExplorer.TextHandling;
using Microsoft.Extensions.DependencyInjection;

var builder = CoconaApp.CreateBuilder();
builder.Services.AddSingleton<ISumInLineCalculator, SumInLineCalculator>();
builder.Services.AddSingleton<IUnprocessedLineHandler, CsvUnprocessedLineHandler>();

builder.Services.AddSingleton<CsvLineParser>(provider => new CsvLineParser(provider.GetService<ISumInLineCalculator>(),
	provider.GetService<IUnprocessedLineHandler>()));


var app = builder.Build();
app.AddCommand("processfile",async (CsvLineParser parser, string path) =>
{
	await MessageDisplay.ParseCsvFileAndDisplayData(parser, path);
});

app.AddCommand(async (CsvLineParser parser) =>
{
	Console.WriteLine("Enter path to CSV file: ");
	var path = Console.ReadLine();

	if (!string.IsNullOrWhiteSpace(path))
	{
		await MessageDisplay.ParseCsvFileAndDisplayData(parser, path);
	}
});

app.Run();
Console.WriteLine("Press Enter to exit...");
Console.ReadLine();

