using CSVConsoleExplorer.Handlers;
using CSVConsoleExplorer.Interfaces;
using CSVConsoleExplorer.TextHandling.Components;
using CSVConsoleExplorer.TextHandling.Extensions;

namespace CSVConsoleExplorer.TextHandling;
public class CsvLineParser
{
	private readonly char _separator = ';';
	private readonly ISumInLineCalculator _sumInLineCalculator;
	private readonly IUnprocessedLineHandler _unprocessedLineHandler;
	private readonly ILinesReceiver _linesReceiver;

	
	public CsvLineParser(ISumInLineCalculator sumInLineCalculator,
		IUnprocessedLineHandler unprocessedLineHandler,
		ILinesReceiver linesReceiver)
	{
		_sumInLineCalculator = sumInLineCalculator;
		_unprocessedLineHandler = unprocessedLineHandler;
		_linesReceiver = linesReceiver;
	}
	public CsvLineParser(ISumInLineCalculator sumInLineCalculator,
		IUnprocessedLineHandler unprocessedLineHandler,
		ILinesReceiver linesReceiver, char separator)
	{
		_sumInLineCalculator = sumInLineCalculator;
		_unprocessedLineHandler = unprocessedLineHandler;
		_linesReceiver = linesReceiver;
		_separator = separator;
	}
	
	public async Task<ParsedDataFromCsvFile> ParseCsvFile(string filePath)
	{
		var i = 0;
		_unprocessedLineHandler.SetHandler(_sumInLineCalculator);
		
		await foreach (var line in _linesReceiver.ReadLines(filePath))
		{
			i++;
			
			var elements = line.Split(_separator).ToList();
			var filteredElements = FilterEmptyElements(elements);
			IAsyncEnumerable<string> elementsAsyncEnumerable = filteredElements.ToAsyncEnumerable();
			var currentLine = new CsvLine(elementsAsyncEnumerable, i);
			
			await _unprocessedLineHandler.HandleLine(currentLine);
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