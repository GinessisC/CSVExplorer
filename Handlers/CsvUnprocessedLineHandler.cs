using System.Numerics;
using CSVConsoleExplorer.TextHandling.Components;
using CSVConsoleExplorer.TextHandling.Extensions;

namespace CSVConsoleExplorer.Handlers;

public class CsvUnprocessedLineHandler<TLineNumber> : LineHandlerBase
	where TLineNumber : INumber<TLineNumber>
{
	

	public IAsyncEnumerable<CsvLine<TLineNumber>?>? UnprocessedCsvLines { get; private set; }
	private CsvLine<TLineNumber>? CurrentCsvLine { get; set; }

	public void SetCurrentLine(CsvLine<TLineNumber>? currentLine)
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
			if (UnprocessedCsvLines == null)
			{
				UnprocessedCsvLines = GetEmptyEnumerable();
			}
			UnprocessedCsvLines = UnprocessedCsvLines.Append(CurrentCsvLine);
		});
		await appendUnprocessedLine;
	}

	private static async IAsyncEnumerable<CsvLine<TLineNumber>?> GetEmptyEnumerable()
	{
		await Task.CompletedTask;
		yield break;
	}
}