using CSVConsoleExplorer.TextHandling.Components;

namespace CSVConsoleExplorer;
public static class MessageDisplayer
{
	public static void DisplayBiggestSumAndLine(long biggestSum, long lineNumber) 
	{
		Console.WriteLine($"Biggest sum: {biggestSum} on the line {lineNumber}");
	}

	public static async Task DisplayLines(IAsyncEnumerable<CsvLine>? lines)
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
}