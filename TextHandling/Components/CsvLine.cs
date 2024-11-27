namespace CSVConsoleExplorer.TextHandling.Components;

public class CsvLine
{
	public IAsyncEnumerable<string> Elements { get; private set; }

	public CsvLine(IAsyncEnumerable<string> elements)
	{
		Elements = elements;
	}
}