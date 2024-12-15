using CsvConsoleExplorer.Interfaces;
using CsvConsoleExplorer.TextHandling.Components;

namespace CsvConsoleExplorer.TextHandling;

public record ParsedDataFromCsvFile
{
	private readonly IUnprocessedLineHandler _unprocessedLineHandler;
	private readonly ISumInLineCalculator _lineWithTheBiggestSum;

	public ParsedDataFromCsvFile(ISumInLineCalculator lineWithTheBiggestSum,
		IUnprocessedLineHandler unprocessedLineHandler)
	{
		_lineWithTheBiggestSum = lineWithTheBiggestSum;
		_unprocessedLineHandler = unprocessedLineHandler;
	}
	
	public IAsyncEnumerable<CsvLine> GetUnprocessedLines()
	{
		return _unprocessedLineHandler.GetUnprocessedCsvLines();
	}
	public CsvLine GetLineWithBiggestSum()
	{
		return _lineWithTheBiggestSum.GetLineWithTheBiggestSum();
	}
}