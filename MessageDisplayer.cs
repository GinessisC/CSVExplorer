using System.Numerics;
using CSVConsoleExplorer.TextHandling.Components;

namespace CSVConsoleExplorer;

public static class MessageDisplayer
{
	public static void DisplayBiggestSumAndLine<TSum, TLineNumber>(TSum biggestSum, TLineNumber lineNumber) 
		where TSum : INumber<TSum>
		where TLineNumber : INumber<TLineNumber>
	{
		Console.WriteLine($"Biggest sum: {biggestSum} on the line {lineNumber}");
	}

	public static async Task DisplayLines<TLineNumber>(IAsyncEnumerable<CsvLine<TLineNumber>>? lines)
		where TLineNumber : INumber<TLineNumber>
	{
		if (lines is not null)
		{
			await foreach (var line in lines)
			{
				Console.WriteLine($"Unprocessed line #{line.LineNumber}:");

				await foreach (var element in line.Elements)
				{
					Console.WriteLine(element);
				}
				Console.WriteLine("\n");
			}
		}
	}
}