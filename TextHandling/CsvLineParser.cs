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
	public CsvUnprocessedLineHandler<TLineNumber>? UnprocessedLineHandler { get; private set; }
	public SumInLineCalculator<TLineNumber, TSum>? LineWithTheBiggestLine { get; private set; }

	public async Task ParseCsvFile(string csvFilePath)
	{
		var i = TLineNumber.Zero;
		CsvUnprocessedLineHandler<TLineNumber> unprocessedLineHandler = new();
		SumInLineCalculator<TLineNumber, TSum> sumInLineCalculator = new();
		unprocessedLineHandler.SetHandler(sumInLineCalculator);
		
		await foreach (var line in File.ReadLinesAsync(csvFilePath))
		{
			i++;
			
			var elements = line.Split(Separator).ToList();
			var filteredElements = FilterUnnecessaryElements(elements);
			IAsyncEnumerable<string>? elementsAsyncEnumerable = filteredElements.ToAsyncEnumerable();
			var currentLine = new CsvLine<TLineNumber>(elementsAsyncEnumerable, i);
			
			unprocessedLineHandler.SetCurrentLine(currentLine);
			sumInLineCalculator.SetCurrentLine(currentLine);

			unprocessedLineHandler.HandleLine();
		}
		UnprocessedLineHandler = unprocessedLineHandler;
		LineWithTheBiggestLine = sumInLineCalculator;
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