using System.Numerics;
using CSVConsoleExplorer.TextHandling.Components;
using CSVConsoleExplorer.TextHandling.Extensions;

namespace CSVConsoleExplorer.Handlers;

public class CsvUnprocessedLineHandler<TLineNumber> : LineHandlerBase 
	where TLineNumber : INumber<TLineNumber>
{
	public IAsyncEnumerable<CsvLine<TLineNumber>?> UnprocessedCsvLines { get; private set; } = default;
	private CsvLine<TLineNumber>? CurrentCsvLine { get; set; } = new(default, TLineNumber.Zero);

	public void SetCurrentLine(CsvLine<TLineNumber>? currentLine)
	{
		CurrentCsvLine = currentLine;
	}
	protected override bool CanHandle()
	{
		return CurrentCsvLine != null && CurrentCsvLine.IsNumerical();
	}

	protected override async Task Handle()
	{
		UnprocessedCsvLines?.Append(CurrentCsvLine);
	}
}