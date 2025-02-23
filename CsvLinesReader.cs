using CsvConsoleExplorer.Interfaces;

namespace CsvConsoleExplorer;

public class CsvLinesReader : ILinesReader
{
	public IAsyncEnumerable<string> ReadLines(string filePath)
	{
		return File.ReadLinesAsync(filePath);
	}
}