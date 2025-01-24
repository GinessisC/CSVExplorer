using CsvConsoleExplorer.Interfaces;
using CsvConsoleExplorer.TextHandling;
using CsvConsoleExplorer.TextHandling.Components;

namespace CsvConsoleExplorer;
public class MessageDisplay : IMessageDisplay
{
	private readonly ICsvLineParser _parser;

	public MessageDisplay(ICsvLineParser parser)
	{
		_parser = parser;
	}
	public async Task ParseCsvFileAndDisplayData(string filePath)
	{
		ParsedDataFromCsvFile parsedData = await _parser.ParseCsvFile(filePath);
		
		CsvLine lineWithBiggestSum = parsedData.GetLineWithBiggestSum();
		long lineNumberOfTheBiggestSum = lineWithBiggestSum.LineNumber;
		IAsyncEnumerable<CsvLine> unprocessedLines = parsedData.GetUnprocessedLines();

		var numbersInLine = lineWithBiggestSum.Elements.Select(int.Parse);

		DisplayBiggestSumAndLine(numbersInLine.Sum(), lineNumberOfTheBiggestSum);

		await DisplayLines(unprocessedLines);
	}
	private void DisplayBiggestSumAndLine(long biggestSum, long lineNumber) 
	{
		Console.WriteLine($"Biggest sum: {biggestSum} on the line {lineNumber}");
	}
	
	private async Task DisplayLines(IAsyncEnumerable<CsvLine> lines)
	{
		await foreach (var line in lines)
		{
			Console.WriteLine($"Unprocessed line #{line.LineNumber}:");
			foreach (var element in line.Elements)
			{
				Console.WriteLine(element);
			}
			Console.WriteLine("\n");
		}
	}
}