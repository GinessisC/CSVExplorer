using Cocona;
using CSVConsoleExplorer;
using CSVConsoleExplorer.TextHandling;

static async Task ParseCsvFileAndDisplayData(string csvFilePath)
{
	var parsedData = await CsvLineParser.ParseCsvFile(csvFilePath, new ConsoleWarningsDisplayer());
		
	var biggestSum = parsedData.GetBiggestLineSumPair().Key;
	var lineNumberOfTheBiggestSum = parsedData.GetBiggestLineSumPair().Value.LineNumber;
	var unprocessedLines = parsedData.GetUnprocessedLines();
		
	MessageDisplayer.DisplayBiggestSumAndLine(biggestSum, lineNumberOfTheBiggestSum);
	await MessageDisplayer.DisplayLines(unprocessedLines);	
}
var builder = CoconaApp.CreateBuilder();

var app = builder.Build();


app.AddCommand(async (string path) =>
{
	await ParseCsvFileAndDisplayData(path);
});
app.AddCommand(async () =>
{
	var path = Console.ReadLine();

	if (path != null)
	{
		await ParseCsvFileAndDisplayData(path);
	}
});

app.Run();