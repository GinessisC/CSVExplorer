namespace CsvConsoleExplorer.Interfaces;

public interface ILinesReceiver
{
	IAsyncEnumerable<string> ReadLines(string filePath);
}