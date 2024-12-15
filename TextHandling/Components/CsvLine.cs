using System.Numerics;

namespace CsvConsoleExplorer.TextHandling.Components;

public class CsvLine
{
	public IAsyncEnumerable<string>? Elements { get; private set; }
	public long LineNumber { get; private set; }
	
	public CsvLine(IAsyncEnumerable<string>? elements, long lineNumber)
	{
		Elements = elements;
		LineNumber = lineNumber;
	}
}