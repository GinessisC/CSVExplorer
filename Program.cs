using System.Runtime.Intrinsics.Arm;
using CSVConsoleExplorer.TextHandling;
using CSVConsoleExplorer.TextHandling.Components;

namespace CSVConsoleExplorer;

class Program
{
	private static async Task EnumerateLines(IAsyncEnumerable<KeyValuePair<int, CsvLine<int>>> csvLines)
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
		CsvLineParser<int, int> parser = new();
		
		if (path != null)
		{
			await parser.ParseCsvFile(path);
		}

		var unprocessedLineHandler = parser.UnprocessedLineHandler;
		var lineWithTheBiggestSum = parser.LineWithTheBiggestLine;

		if (lineWithTheBiggestSum != null)
		{
			Console.WriteLine($"Line number: {lineWithTheBiggestSum.LineWithTheBiggestSum.LineNumber}Biggest sum: {lineWithTheBiggestSum.BiggestSumInLines}");
		}

		Console.WriteLine("Unprocessed lines:\n");
		
		if (unprocessedLineHandler != null)
		{
			await foreach (var line in unprocessedLineHandler.UnprocessedCsvLines)
			{
				if (line == null)
				{
					continue;
				}

				if (line.Elements != null)
				{
					await foreach (var element in line.Elements)
					{
						Console.WriteLine(element);
					}
				}
			}
		}
	}
}