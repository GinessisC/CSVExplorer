using CSVConsoleExplorer.Handlers;
using CSVConsoleExplorer.Interfaces;
using CSVConsoleExplorer.TextHandling.Components;
using CSVConsoleExplorer.TextHandling.Extensions;

namespace CSVConsoleExplorer.TextHandling;
public class CsvLineParser
{
	private const string Separator = ";";
	private readonly ISumInLineCalculator _sumInLineCalculator;
	private readonly IUnprocessedLineHandler _unprocessedLineHandler;
	
	public CsvLineParser(ISumInLineCalculator sumInLineCalculator, IUnprocessedLineHandler unprocessedLineHandler)
	{
		_sumInLineCalculator = sumInLineCalculator;
		_unprocessedLineHandler = unprocessedLineHandler;
	}
	
	public async Task<ParsedDataFromCsvFile> ParseCsvFile(string csvFilePath)
	{ 
		var i = 0;
		_unprocessedLineHandler.SetHandler(_sumInLineCalculator);

		await foreach (var line in File.ReadLinesAsync(csvFilePath))
		{
			i++;
			
			var elements = line.Split(Separator).ToList();
			var filteredElements = FilterEmptyElements(elements);
			IAsyncEnumerable<string> elementsAsyncEnumerable = filteredElements.ToAsyncEnumerable();
			var currentLine = new CsvLine(elementsAsyncEnumerable, i);
			
			_unprocessedLineHandler.SetCurrentLine(currentLine);
			_sumInLineCalculator.SetCurrentLine(currentLine);
			
			await _unprocessedLineHandler.HandleLine();
		}
		return new ParsedDataFromCsvFile(_sumInLineCalculator, _unprocessedLineHandler);
	}
	private static IEnumerable<string> FilterEmptyElements(List<string> elements)
	{
		if (elements.All(string.IsNullOrEmpty))
		{
			return elements;
		}
		return elements
			.Where(element => !string.IsNullOrEmpty(element));
	}
}