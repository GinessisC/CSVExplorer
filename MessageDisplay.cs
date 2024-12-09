using CSVConsoleExplorer.TextHandling;
using CSVConsoleExplorer.TextHandling.Components;

namespace CSVConsoleExplorer;
public static class MessageDisplay
{
	private static void DisplayBiggestSumAndLine(long biggestSum, long lineNumber) 
	{
		Console.WriteLine($"Biggest sum: {biggestSum} on the line {lineNumber}");
	}
	
	private static async Task DisplayLines(IAsyncEnumerable<CsvLine>? lines)
	{
		if (lines is not null)
		{
			await foreach (var line in lines)
			{
				if (line.Elements == null)
				{
					continue;
				}
				
				Console.WriteLine($"Unprocessed line #{line.LineNumber}:");
				await foreach (var element in line.Elements)
				{
					Console.WriteLine(element);
				}
				Console.WriteLine("\n");
			}
		}
	}
	public static async Task ParseCsvFileAndDisplayData(CsvLineParser parser, string csvFilePath)
	{
		ParsedDataFromCsvFile parsedData = await parser.ParseCsvFile(csvFilePath);
	
		CsvLine lineWithBiggestSum = parsedData.GetLineWithBiggestSum();
		long lineNumberOfTheBiggestSum = lineWithBiggestSum.LineNumber;
		IAsyncEnumerable<CsvLine> unprocessedLines = parsedData.GetUnprocessedLines();
		
		if (lineWithBiggestSum.Elements != null)
		{
			var numbersInLine = lineWithBiggestSum.Elements.ToBlockingEnumerable().Select(int.Parse);

			DisplayBiggestSumAndLine(numbersInLine.Sum(), lineNumberOfTheBiggestSum);
		}

		await DisplayLines(unprocessedLines);
	}
}