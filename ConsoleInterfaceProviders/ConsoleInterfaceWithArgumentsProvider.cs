using Cocona;

namespace CSVConsoleExplorer.ConsoleInterfaceProviders;

public class ConsoleInterfaceWithArgumentsProvider : InterfaceProviderBase
{
	private readonly string _pathToCsvFile;
	public ConsoleInterfaceWithArgumentsProvider(string pathToCsvFile)
	{
		_pathToCsvFile = pathToCsvFile;
	}
	public override async Task RunAsync()
	{
		await CoconaApp.RunAsync(async ([Argument(Description = "Path to csv file")] string path,
			[Argument(Description = "file size of csv file: Small or Big (s/b)")]
			string filesize) =>
		{
			FileSize fileSize = FileSize.Small;

			if (filesize.Contains('b', StringComparison.CurrentCultureIgnoreCase))
			{
				fileSize = FileSize.Big;
			}
			var parser = ParseFactory.DefineParser(fileSize);

			await parser.ParseCsvFile(_pathToCsvFile);

			var biggestSum = parser.GetBiggestLineSumPair().Key;
			var lineNumberOfTheBiggestSum = parser.GetBiggestLineSumPair().Value.LineNumber;
			var unprocessedLines = parser.GetUnprocessedLines().Elements;

			MessageDisplayer.DisplayBiggestSumAndLine(biggestSum, lineNumberOfTheBiggestSum);
			if (unprocessedLines != null)
			{
				await MessageDisplayer.DisplayLines(unprocessedLines);
			}
		});
	}
}