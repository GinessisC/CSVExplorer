using System.Numerics;
using CSVConsoleExplorer.Handlers;
using CSVConsoleExplorer.TextHandling.Components;
using CSVConsoleExplorer.TextHandling.Extensions;

namespace CSVConsoleExplorer.TextHandling;

public class CsvLineParser<TLineNumber, TSum> 
	where TSum : INumber<TSum>
	where TLineNumber : INumber<TLineNumber>
{
	private const string Separator = ",";
	private CsvUnprocessedLineHandler<TLineNumber>? UnprocessedLineHandler { get; set; }
	private SumInLineCalculator<TLineNumber, TSum>? LineWithTheBiggestSum { get; set; }

	public IAsyncEnumerable<CsvLine<TLineNumber>> GetUnprocessedLines()
	{
		if (UnprocessedLineHandler == null)
		{
			throw new NotImplementedException("Unprocessed lines are not identified yet");
		}
		return UnprocessedLineHandler.UnprocessedCsvLines!;
	}
	public KeyValuePair<TSum, CsvLine<TLineNumber>> GetBiggestLineSumPair()
	{
		if (LineWithTheBiggestSum == null)
		{
			throw new NotImplementedException("Line with the biggest sum is not identified yet");
		}
		TSum sum = LineWithTheBiggestSum.BiggestSumInLines;
		var line = LineWithTheBiggestSum.LineWithTheBiggestSum;
		var biggestLineSumPair = new KeyValuePair<TSum, CsvLine<TLineNumber>>(sum, line);
		
		return biggestLineSumPair;
	}
	public async Task ParseCsvFile(string csvFilePath)
	{ 
		var i = TLineNumber.Zero;
		CsvUnprocessedLineHandler<TLineNumber> unprocessedLineHandler = new();
		SumInLineCalculator<TLineNumber, TSum>? sumInLineCalculator = new();
		unprocessedLineHandler.SetHandler(sumInLineCalculator);
		
		foreach (var line in File.ReadLines(csvFilePath))
		{
			i++;
			
			var elements = line.Split(Separator).ToList();
			var filteredElements = FilterUnnecessaryElements(elements);
			IAsyncEnumerable<string> elementsAsyncEnumerable = filteredElements.ToAsyncEnumerable();
			var currentLine = new CsvLine<TLineNumber>(elementsAsyncEnumerable, i);
			
			unprocessedLineHandler.SetCurrentLine(currentLine);
			sumInLineCalculator.SetCurrentLine(currentLine);
			
			await unprocessedLineHandler.HandleLine();
		}
		UnprocessedLineHandler = unprocessedLineHandler;
		LineWithTheBiggestSum = sumInLineCalculator;
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