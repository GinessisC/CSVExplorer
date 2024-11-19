using CSVFileReaders;
using CsvHandling;

namespace CSVConsoleExplorer;

class Program
{
	static async Task Main(string[] args)
	{
		string? path = Console.ReadLine();

		if (string.IsNullOrEmpty(path))
		{
			Console.WriteLine("Please specify a file path");
			return;
		}
		
		CsvFileReader reader = new(path);
		var fileData = await reader.GetFileLinesAsync();
		NumberSorter numberSorter = new(fileData);
		SumCounter lh = new(numberSorter);
		
		int maxSum = lh.GetMaxSumInLines();
		Console.WriteLine($"Line: {lh.MaxSumLine} \t Value: {maxSum}");
	}
}