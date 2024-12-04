using CSVConsoleExplorer.TextHandling;

namespace CSVConsoleExplorer.ConsoleInterfaceProviders;
public class ConsoleInterfaceProvider
{
	private readonly string _pathToCsvFile;

	public ConsoleInterfaceProvider(string pathToCsvFile)
	{
		_pathToCsvFile = pathToCsvFile;
	}
    
	public async Task RunAsync()
	{
		var parser = new CsvLineParser();

		await parser.ParseCsvFile(_pathToCsvFile);

		var biggestSum = parser.GetBiggestLineSumPair().Key;
		var lineNumberOfTheBiggestSum = parser.GetBiggestLineSumPair().Value.LineNumber;
		var unprocessedLines = parser.GetUnprocessedLines();

		MessageDisplayer.DisplayBiggestSumAndLine(biggestSum, lineNumberOfTheBiggestSum);
		await MessageDisplayer.DisplayLines(unprocessedLines);
	}
}