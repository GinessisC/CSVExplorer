namespace CSVConsoleExplorer.ConsoleInterfaceProviders;

public class ConsoleInterfaceProvider : InterfaceProviderBase
{
	private readonly string _pathToCsvFile;

	public ConsoleInterfaceProvider(string pathToCsvFile)
	{
		_pathToCsvFile = pathToCsvFile;
	}
    
	public override async Task RunAsync()
	{
		var fileSize = AskForFileSize();
		
		var parser = ParseFactory.DefineParser(fileSize);

		await parser.ParseCsvFile(_pathToCsvFile);

		var biggestSum = parser.GetBiggestLineSumPair().Key;
		var lineNumberOfTheBiggestSum = parser.GetBiggestLineSumPair().Value.LineNumber;
		var unprocessedLines = parser.GetUnprocessedLines();

		MessageDisplayer.DisplayBiggestSumAndLine(biggestSum, lineNumberOfTheBiggestSum);

		if (unprocessedLines != null)
		{
			await MessageDisplayer.DisplayLines(unprocessedLines);
		}
	}

	private FileSize AskForFileSize()
	{
		FileSize fileSize = FileSize.Small;
		Console.WriteLine("Please enter a file size: Small/Big (s/b): ");
		var inputFileSize = Console.ReadLine();
		
		if (inputFileSize != null && inputFileSize.ToLower().Contains('b'))
		{
			fileSize = FileSize.Big;
		}
		return fileSize;
	}
}