namespace CsvConsoleExplorer.Interfaces;

public interface ILinesReader
{
	IAsyncEnumerable<string> ReadLines(string filePath);
}