using CsvConsoleExplorer.FileParsing.Components;
using CsvConsoleExplorer.FileParsing.Extensions;
using CsvConsoleExplorer.Interfaces;

namespace CsvConsoleExplorer.Handlers;
public class CsvUnprocessedLineHandler : LineHandlerBase, IUnprocessedLineHandler
{
	private IAsyncEnumerable<CsvLine> _unprocessedCsvLines = GetEmptyEnumerable();

	public IAsyncEnumerable<CsvLine> GetUnprocessedLines()
	{
		return _unprocessedCsvLines;
	}
	protected override bool CanHandle(CsvLine line)
	{
		return line.IsNumerical() is false;
	}
	
	protected override async Task Handle(CsvLine line)
	{
		await Task.Run(() =>
		{
			_unprocessedCsvLines = _unprocessedCsvLines.Append(line);
		});
	}

	private static async IAsyncEnumerable<CsvLine> GetEmptyEnumerable()
	{
		await Task.CompletedTask;
		yield break;
	}
}