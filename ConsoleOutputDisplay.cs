using CsvConsoleExplorer.FileParsing;
using CsvConsoleExplorer.Interfaces;
using CsvConsoleExplorer.FileParsing.Components;

namespace CsvConsoleExplorer;
public class ConsoleOutputDisplay : IOutputDisplay
{
	private readonly ICsvLineParser _parser;

	public ConsoleOutputDisplay(ICsvLineParser parser)
	{
		_parser = parser;
	}
	public async Task DisplayParsedFileData(string filePath)
	{
		ParsedDataFromCsvFile parsedData = await _parser.ParseCsvFile(filePath);
		
		DisplayBiggestSumAndLine(parsedData.GetLineWithBiggestSum());
		await DisplayUnprocessedLines(parsedData.GetUnprocessedLines());
	}
	private void DisplayBiggestSumAndLine(CsvLine lineWithBiggestSum) 
	{
		var numbersInLine = lineWithBiggestSum.Elements.Select(int.Parse);
		Console.WriteLine($"Biggest sum: {numbersInLine.Sum()} on the line {lineWithBiggestSum.LineNumber}");
	}
	private async Task DisplayUnprocessedLines(IAsyncEnumerable<CsvLine> unprocessedLines)
	{
		Console.WriteLine($"Unprocessed lines:");
		await foreach (var line in unprocessedLines)
		{
			DisplayLine(line);
		}
	}
	private void DisplayLine(CsvLine line)
	{
		Console.WriteLine($"Unprocessed line #{line.LineNumber}:");
		foreach (var element in line.Elements)
		{
			Console.WriteLine(element);
		}
	}
}