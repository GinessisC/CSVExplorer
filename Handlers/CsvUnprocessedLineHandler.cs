using CSVConsoleExplorer.Interfaces;
using CSVConsoleExplorer.TextHandling.Components;
using CSVConsoleExplorer.TextHandling.Extensions;

namespace CSVConsoleExplorer.Handlers;
public class CsvUnprocessedLineHandler : LineHandlerBase, IUnprocessedLineHandler
{
	private IAsyncEnumerable<CsvLine> _unprocessedCsvLines = GetEmptyEnumerable();
	private CsvLine? _currentCsvLine;

	public override void SetCurrentLine(CsvLine currentLine)
	{
		_currentCsvLine = currentLine;
	}

	public IAsyncEnumerable<CsvLine> GetUnprocessedCsvLines()
	{
		return _unprocessedCsvLines;
	}

	protected override bool CanHandle()
	{
		return _currentCsvLine != null && !_currentCsvLine.IsNumerical();
	}
	
	protected override async Task Handle()
	{
		Task appendUnprocessedLine = Task.Run(() =>
		{
			_unprocessedCsvLines = _unprocessedCsvLines.Append(_currentCsvLine);
		});
		await appendUnprocessedLine;
	}

	private static async IAsyncEnumerable<CsvLine> GetEmptyEnumerable()
	{
		await Task.CompletedTask;
		yield break;
	}
}