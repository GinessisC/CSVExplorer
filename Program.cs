using CsvHandling;

namespace CSVConsoleExplorer;

class Program
{
	public static async Task Main(string[] args)
	{
		string? path = Console.ReadLine();

		if (string.IsNullOrEmpty(path))
		{
			Console.WriteLine("Please specify a file path");
			return;
		}

		CsvLineLinesParser lineParser = new(path);
		var parsedData = lineParser.HandleLines();
		
		//CsvUnprocessedLineHandler unprocessedLineHandler = new(parsedData);
		//var unpData = unprocessedLineHandler.HandleLines();
		SumInLineCounter sumInLineCounter = new(parsedData);
		//lineParser.SetNextHandler(unprocessedLineHandler);

		int biggestSum = await sumInLineCounter.GetBiggestSumAsync();
		
		
		Console.WriteLine(biggestSum);
		Console.WriteLine($"The line with the biggest sum is {sumInLineCounter.IndexOfLineWithBiggestSum}");
	}
}