namespace CSVConsoleExplorer.Interfaces;

public interface ILinesReceiver
{
	IAsyncEnumerable<string> ReadLines(string path);
}