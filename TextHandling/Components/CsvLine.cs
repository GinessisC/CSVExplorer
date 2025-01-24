using System.Numerics;

namespace CsvConsoleExplorer.TextHandling.Components;

public class CsvLine
{
	public IEnumerable<string> Elements { get; private set; }
	public long LineNumber { get; private set; }
	
	public CsvLine(IEnumerable<string> elements, long lineNumber)
	{
		Elements = elements;
		LineNumber = lineNumber;
	}
}