using CSVConsoleExplorer.TextHandling;
using CSVConsoleExplorer.TextHandling.Components;

namespace CSVConsoleExplorer;

class Program
{
	private static async Task EnumerateLines(IAsyncEnumerable<KeyValuePair<int, CsvLine>> csvLines)
	{
		await foreach (var countLinePair in csvLines)
		{
			var unprocessedElements = countLinePair.Value.Elements;
			Console.WriteLine($"Line {countLinePair.Key}:");

			await foreach (var element in unprocessedElements)
			{
				Console.WriteLine($"\t{element}");
			}
		}
	}
	public static async Task Main(string[] args)
	{
		string? path = Console.ReadLine();
		var fileElements = CsvLineLinesParser.ParseCsvFile(path);
		NumericalLineHandler numericalLineHandler = new(fileElements);
		var result = await numericalLineHandler.GetLineNumberMaxSumPair();
		
		Console.WriteLine($"Line: {result.Key}\nMaxSum: {result.Value}");
		CsvUnprocessedLineHandler unprocessedLineHandler = new(fileElements);
		IAsyncEnumerable<KeyValuePair<int, CsvLine>> a = unprocessedLineHandler.GetUnprocessedCsvLines();
		
		Console.WriteLine("Unprocessed lines:\n");
		await EnumerateLines(a);
	}
}