using Cocona;
using CsvConsoleExplorer;
using CsvConsoleExplorer.Handlers;
using CsvConsoleExplorer.Interfaces;
using CsvConsoleExplorer.TextHandling;
using Microsoft.Extensions.DependencyInjection;

var builder = CoconaApp.CreateBuilder();

builder.Services.AddSingleton<ISumInLineCalculator, SumInLineCalculator>();
builder.Services.AddSingleton<IUnprocessedLineHandler, CsvUnprocessedLineHandler>();
builder.Services.AddSingleton<ILinesReceiver, LinesFromFileReceiver>();
builder.Services.AddSingleton<CsvLineParser>();

builder.Services.AddSingleton<MessageDisplay>();


var app = builder.Build();
app.AddCommand("processfile",async (string filePath,
	MessageDisplay display) =>
{
	await display.ParseCsvFileAndDisplayData(filePath);
});

app.AddCommand(async (MessageDisplay display) =>
{
	Console.WriteLine("Enter path to CSV file: ");
	var path = Console.ReadLine();
	
	if (!string.IsNullOrWhiteSpace(path))
	{
		await display.ParseCsvFileAndDisplayData(path);
	}
});

await app.RunAsync();
Console.WriteLine("Press Enter to exit...");
Console.ReadLine();

