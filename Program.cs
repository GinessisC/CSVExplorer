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

var app = builder.Build();
app.AddCommand("processfile",async (string filePath,
	IOutputDisplay display) =>
{
	await TryProcessAsync(display, filePath);
});

app.AddCommand(async (IOutputDisplay display) =>
{
	Console.WriteLine("Enter path to CSV file: ");
	var filePath = Console.ReadLine();
	
	await TryProcessAsync(display, filePath);
});

await app.RunAsync();

static async Task TryProcessAsync(IOutputDisplay display, string path)
{
	try
	{
		await ProcessAsync(display, path);
	}
	catch (Exception e)
	{
		Console.WriteLine(e);
	}
}

static async Task ProcessAsync(IOutputDisplay display, string path)
{
	if (string.IsNullOrWhiteSpace(path) is false)
	{
		await display.DisplayParsedFileData(path);
	}
}