using System.Numerics;

namespace CSVConsoleExplorer.TextHandling.Components;

public class CsvLine<TLineNumber> 
	where TLineNumber : INumber<TLineNumber>
{
	public IAsyncEnumerable<string>? Elements { get; private set; }
	public TLineNumber LineNumber { get; private set; }
	
	public CsvLine(IAsyncEnumerable<string>? elements, TLineNumber lineNumber)
	{
		Elements = elements;
		LineNumber = lineNumber;
	}
}