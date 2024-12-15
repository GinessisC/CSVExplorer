using CSVConsoleExplorer.Handlers;
using CSVConsoleExplorer.Interfaces;
using CSVConsoleExplorer.TextHandling.Components;

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
		_unprocessedLineHandler.SetHandler(_sumInLineCalculator);
		
		var i = 0;
		await foreach (var line in _linesReceiver.ReadLines(filePath))
		{
			i++;

			var currentLine = ConvertToCsvLine(line, i);
			await _unprocessedLineHandler.HandleLine(currentLine);
		} 
		return new ParsedDataFromCsvFile(_sumInLineCalculator, _unprocessedLineHandler);
	}

	private CsvLine ConvertToCsvLine(string line, int lineNumber)
	{
		var elements = line.Split(_separator).ToList();
		var filteredElements = FilterEmptyElements(elements);
		IAsyncEnumerable<string> elementsAsyncEnumerable = filteredElements.ToAsyncEnumerable();
		return new CsvLine(elementsAsyncEnumerable, lineNumber);
	}
	private IEnumerable<string> FilterEmptyElements(List<string> elements)
	{
		if (elements.All(string.IsNullOrEmpty))
		{
			return elements;
		}
		return elements
			.Where(element => !string.IsNullOrEmpty(element));
	}
}