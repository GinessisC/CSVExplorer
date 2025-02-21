using Cocona;
using CsvConsoleExplorer;
using CsvConsoleExplorer.FileParsing;
using CsvConsoleExplorer.Handlers;
using CsvConsoleExplorer.Interfaces;
using Microsoft.Extensions.DependencyInjection;

var builder = CoconaApp.CreateBuilder();

builder.Services.AddTransient<ISumInLineCalculator, SumInLineCalculator>();
builder.Services.AddTransient<IUnprocessedLineHandler, CsvUnprocessedLineHandler>();
builder.Services.AddScoped<ILinesReceiver, LinesFromFileReceiver>();
builder.Services.AddTransient<ICsvLineParser, CsvLineParser>();

builder.Services.AddTransient<IMessageDisplay, MessageDisplay>();


var app = builder.Build();
app.AddCommand("processfile",async (string filePath,
	MessageDisplay display) =>
{
	await display.ParseCsvFileAndDisplayData(filePath);
	await app.RunAsync();

});

app.AddCommand(async (IMessageDisplay display) =>
{
	Console.WriteLine("Enter path to CSV file: ");
	var path = Console.ReadLine();
	
	if (!string.IsNullOrWhiteSpace(path))
	{
		await display.ParseCsvFileAndDisplayData(path);
	}

});

await app.RunAsync();