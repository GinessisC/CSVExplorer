using CSVConsoleExplorer.Interfaces;
using CSVConsoleExplorer.TextHandling.Components;
using CSVConsoleExplorer.TextHandling.Extensions;

namespace CSVConsoleExplorer.Handlers;
public class CsvUnprocessedLineHandler : LineHandlerBase, IUnprocessedLineHandler
{
	private IAsyncEnumerable<CsvLine> _unprocessedCsvLines = GetEmptyEnumerable();

	public IAsyncEnumerable<CsvLine> GetUnprocessedCsvLines()
	{
		return _unprocessedCsvLines;
	}
	protected override bool CanHandle(CsvLine line)
	{
		return line.IsNumerical() is false;
	}
	
	protected override async Task Handle(CsvLine line)
	{
		Task appendUnprocessedLine = Task.Run(() =>
		{
			_unprocessedCsvLines = _unprocessedCsvLines.Append(line);
		});
		await appendUnprocessedLine;
	}

	private static async IAsyncEnumerable<CsvLine> GetEmptyEnumerable()
	{
		await Task.CompletedTask;
		yield break;
	}
}