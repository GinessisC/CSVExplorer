namespace CSVConsoleExplorer.TextHandling.Components;

public class CsvLine
{
	public bool CanBeHandled { get; private set; }
	public IAsyncEnumerable<string> Elements { get; private set; }

	public CsvLine(IAsyncEnumerable<string> elements, bool canBeHandled)
	{
		Elements = elements;
		CanBeHandled = canBeHandled;
	}
}