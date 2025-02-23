namespace CsvConsoleExplorer.FileParsing.Components;
public class CsvLine
{
	public IEnumerable<string> Elements { get; }
	public long LineNumber { get; }
	
	public CsvLine(IEnumerable<string> elements, long lineNumber)
	{
		Elements = elements;
		LineNumber = lineNumber;
	}
}