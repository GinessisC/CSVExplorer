using CsvConsoleExplorer.Interfaces;
using CsvConsoleExplorer.TextHandling.Components;

namespace CsvConsoleExplorer.TextHandling;
public class CsvLineParser : ICsvLineParser
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
		var elementsAsyncEnumerable = elements;
		return new CsvLine(elementsAsyncEnumerable, lineNumber);
	}
}