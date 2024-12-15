using CsvConsoleExplorer.Interfaces;

namespace CsvConsoleExplorer;

public class LinesFromFileReceiver : ILinesReceiver
{
	public IAsyncEnumerable<string> ReadLines(string filePath)
	{
		return File.ReadLinesAsync(filePath);
	}
}