using Cocona;
using CSVConsoleExplorer;
using CSVConsoleExplorer.TextHandling;

var builder = CoconaApp.CreateBuilder();

var app = builder.Build();

//TODO: add command with parameters

app.AddCommand(async () =>
{
	var path = Console.ReadLine();

	if (path != null)
	{
		var parsedData = await CsvLineParser.ParseCsvFile(path, new ConsoleWarningsDisplayer());
		
		var biggestSum = parsedData.GetBiggestLineSumPair().Key;
		var lineNumberOfTheBiggestSum = parsedData.GetBiggestLineSumPair().Value.LineNumber;
		var unprocessedLines = parsedData.GetUnprocessedLines();
		
		MessageDisplayer.DisplayBiggestSumAndLine(biggestSum, lineNumberOfTheBiggestSum);
		await MessageDisplayer.DisplayLines(unprocessedLines);
	}
});

app.Run();