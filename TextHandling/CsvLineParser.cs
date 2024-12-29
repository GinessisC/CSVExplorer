using CSVConsoleExplorer.Handlers;
using CSVConsoleExplorer.Interfaces;
using CSVConsoleExplorer.TextHandling.Components;
using CSVConsoleExplorer.TextHandling.Extensions;

namespace CSVConsoleExplorer.TextHandling;
public static class CsvLineParser
{
	private const string Separator = ",";
	
	public static async Task<ParsedDataFromCsvFile> ParseCsvFile(string csvFilePath, IWarningsDisplayer warningsDisplayer)
	{ 
		var i = 0;
		CsvUnprocessedLineHandler unprocessedLineHandler = new(warningsDisplayer);
		SumInLineCalculator sumInLineCalculator = new(warningsDisplayer);
		unprocessedLineHandler.SetHandler(sumInLineCalculator);
		
		foreach (var line in File.ReadLines(csvFilePath))
		{
			i++;
			
			var elements = line.Split(Separator).ToList();
			var filteredElements = FilterUnnecessaryElements(elements);
			IAsyncEnumerable<string> elementsAsyncEnumerable = filteredElements.ToAsyncEnumerable();
			var currentLine = new CsvLine(elementsAsyncEnumerable, i);
			
			unprocessedLineHandler.SetCurrentLine(currentLine);
			sumInLineCalculator.SetCurrentLine(currentLine);
			
			await unprocessedLineHandler.HandleLine();
		}
		
		return new ParsedDataFromCsvFile(sumInLineCalculator, unprocessedLineHandler);
	}
	private static IEnumerable<string> FilterUnnecessaryElements(List<string> elements)
	{
		if (elements.All(string.IsNullOrEmpty))
		{
			return elements;
		}
		
		IEnumerable<string> filteredLine = elements
			.Select(element => element)
			.Where(element => element != string.Empty);
		
		return filteredLine;
	}
}