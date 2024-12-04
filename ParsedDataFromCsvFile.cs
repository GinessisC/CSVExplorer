using CSVConsoleExplorer.Handlers;
using CSVConsoleExplorer.TextHandling.Components;

namespace CSVConsoleExplorer;

public class ParsedDataFromCsvFile
{
	private readonly CsvUnprocessedLineHandler? _unprocessedLineHandler;
	private readonly SumInLineCalculator? _lineWithTheBiggestSum;

	public ParsedDataFromCsvFile(SumInLineCalculator? lineWithTheBiggestSum,
		CsvUnprocessedLineHandler? unprocessedLineHandler)
	{
		_lineWithTheBiggestSum = lineWithTheBiggestSum;
		_unprocessedLineHandler = unprocessedLineHandler;
	}
	
	public IAsyncEnumerable<CsvLine> GetUnprocessedLines()
	{
		if (_unprocessedLineHandler == null)
		{
			throw new ArgumentException("Unprocessed lines are not identified yet");
		}
		return _unprocessedLineHandler.UnprocessedCsvLines!;
	}
	public KeyValuePair<long, CsvLine> GetBiggestLineSumPair()
	{
		if (_lineWithTheBiggestSum == null)
		{
			throw new ArgumentException("Line with the biggest sum is not identified yet");
		}
		var sum = _lineWithTheBiggestSum.BiggestSumInLines;
		var line = _lineWithTheBiggestSum.LineWithTheBiggestSum;
		var biggestLineSumPair = new KeyValuePair<long, CsvLine>(sum, line);
		
		return biggestLineSumPair;
	}	
}