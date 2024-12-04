using Cocona;
using CSVConsoleExplorer.TextHandling;

namespace CSVConsoleExplorer.ConsoleInterfaceProviders;
public class ConsoleInterfaceWithArgumentsProvider
{
	private readonly string _pathToCsvFile;
	public ConsoleInterfaceWithArgumentsProvider(string pathToCsvFile)
	{
		_pathToCsvFile = pathToCsvFile;
	}
	public async Task RunAsync()
	{
		await CoconaApp.RunAsync(async ([Argument(Description = "Path to csv file")] string path) =>
		{
			var parser = new CsvLineParser();
			
			await parser.ParseCsvFile(_pathToCsvFile);

			var biggestSum = parser.GetBiggestLineSumPair().Key;
			var lineNumberOfTheBiggestSum = parser.GetBiggestLineSumPair().Value.LineNumber;
			var unprocessedLines = parser.GetUnprocessedLines();

			MessageDisplayer.DisplayBiggestSumAndLine(biggestSum, lineNumberOfTheBiggestSum);
			await MessageDisplayer.DisplayLines(unprocessedLines);
		});
	}
}