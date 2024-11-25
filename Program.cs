using CSVConsoleExplorer.TextHandling;

namespace CSVConsoleExplorer;

class Program
{
	public static async Task Main(string[] args)
	{
		string? path = Console.ReadLine();
		var kvp = KeyValuePair.Create(1, "CSV File");
		
		KeyValuePair<int, string> kvp2 = new(1, "CSV File");
		Console.WriteLine(kvp2.Key);
		Console.WriteLine(kvp2.Value);
		
		// if (string.IsNullOrEmpty(path))
		// {
		// 	Console.WriteLine("Please specify a file path");
		// 	return;
		// }
		//
		// CsvLineLinesParser lineParser = new(path);
		// var parsedData = lineParser.HandleLines();
		//
		// //CsvUnprocessedLineHandler unprocessedLineHandler = new(parsedData);
		// //var unpData = unprocessedLineHandler.HandleLines();
		// NumericalLineHandler sumInLineCounter = new(parsedData);
		// //lineParser.SetNextHandler(unprocessedLineHandler);
		//
		// int biggestSum = await sumInLineCounter.GetLineNumberMaxSumPair();
		//
		//
		// Console.WriteLine(biggestSum);
		// Console.WriteLine($"The line with the biggest sum is {sumInLineCounter.IndexOfLineWithBiggestSum}");
	}
}