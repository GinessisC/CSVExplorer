using CSVConsoleExplorer.Interfaces;

namespace CSVConsoleExplorer;

public class LinesFromFileReceiver : ILinesReceiver
{
	public IAsyncEnumerable<string> ReadLines(string filePath)
	{
		return File.ReadLinesAsync(filePath);
	}
}