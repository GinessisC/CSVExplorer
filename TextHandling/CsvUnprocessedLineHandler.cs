using CsvHandling.Components;
using CsvHandling.Interfaces;
using System.Collections.Generic;
namespace CsvHandling;

public class CsvUnprocessedLineHandler : CsvLineLinesHandlerBase
{
	private readonly IAsyncEnumerable<CsvLine> _csvLines;

	public CsvUnprocessedLineHandler(IAsyncEnumerable<CsvLine> csvLines)
	{
		_csvLines = csvLines;
	}

	protected override bool CanHandleLines()
	{
		return _csvLines.ToBlockingEnumerable().Any();
	}

	protected override async IAsyncEnumerable<CsvLine> GetHandledResult()
	{
		await foreach (var line in _csvLines)
		{
			if (line.CanBeHandled)
			{
				yield return line;
			}
		}
	}
}