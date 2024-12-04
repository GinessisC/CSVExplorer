using CSVConsoleExplorer.Interfaces;
using CSVConsoleExplorer.TextHandling.Components;
using CSVConsoleExplorer.TextHandling.Extensions;

namespace CSVConsoleExplorer.Handlers;
public class CsvUnprocessedLineHandler : LineHandlerBase
{
	public CsvUnprocessedLineHandler(IWarningsDisplayer warningsDisplayer) : base(warningsDisplayer)
	{
	}

	public IAsyncEnumerable<CsvLine?> UnprocessedCsvLines { get; private set; } = GetEmptyEnumerable();
	private CsvLine? CurrentCsvLine { get; set; }

	public void SetCurrentLine(CsvLine currentLine)
	{
		CurrentCsvLine = currentLine;
	}
	protected override bool CanHandle()
	{
		return CurrentCsvLine != null && !CurrentCsvLine.IsNumerical();
	}
	
	protected override async Task Handle()
	{
		Task appendUnprocessedLine = Task.Run(() =>
		{
			UnprocessedCsvLines = UnprocessedCsvLines.Append(CurrentCsvLine);
		});
		await appendUnprocessedLine;
	}

	private static async IAsyncEnumerable<CsvLine> GetEmptyEnumerable()
	{
		await Task.CompletedTask;
		yield break;
	}
}