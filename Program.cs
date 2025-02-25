using Cocona;
using CsvConsoleExplorer;
using CsvConsoleExplorer.FileParsing;
using CsvConsoleExplorer.Handlers;
using CsvConsoleExplorer.Interfaces;
using Microsoft.Extensions.DependencyInjection;

var builder = CoconaApp.CreateBuilder();

builder.Services.AddTransient<ISumInLineCalculator, SumInLineCalculator>();
builder.Services.AddTransient<IUnprocessedLineHandler, CsvUnprocessedLineHandler>();
builder.Services.AddScoped<ILinesReader, CsvLinesReader>();
builder.Services.AddTransient<ICsvLineParser, CsvLineParser>();
builder.Services.AddTransient<IOutputDisplay, ConsoleOutputDisplay>();
builder.Services.AddTransient<IAppProcessor, AppProcessor>();

var app = builder.Build();
app.AddCommand("processfile",async (string filePath,
	IAppProcessor processor) =>
{
	await processor.TryProcessAsync(filePath);
});

app.AddCommand(async (IAppProcessor processor) =>
{
	Console.WriteLine("Enter path to CSV file: ");
	var filePath = Console.ReadLine();
	
	await processor.TryProcessAsync(filePath);
});

await app.RunAsync();
