using CSVConsoleExplorer.TextHandling.Components;

namespace CSVConsoleExplorer.TextHandling;

public class CsvUnprocessedLineHandlerErr
{
	private readonly IAsyncEnumerable<CsvLine> _csvLines;
	public CsvUnprocessedLineHandlerErr(IAsyncEnumerable<CsvLine> csvLines)
	{
		_csvLines = csvLines;
	}
	public async IAsyncEnumerable<KeyValuePair<int, CsvLine>> GetUnprocessedCsvLines()
	{
		int i = 0;
		await foreach (var line in _csvLines)
		{
			i++;
			if (await IsLineUnprocessed(line))
			{
				yield return new KeyValuePair<int, CsvLine>(i, line);
			}
		}
	}

	private async Task<bool> IsLineUnprocessed(CsvLine line)
	{
		bool isLineUnprocessed = false;
		await foreach (var element in line.Elements)
		{
			if (!IsNumber(element))
			{
				isLineUnprocessed = true;
			}
		}
		return isLineUnprocessed;
	}
	private bool IsNumber(string element)
	{
		return element.All(char.IsDigit);
	}
}