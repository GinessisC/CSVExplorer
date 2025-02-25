namespace CsvConsoleExplorer.Interfaces;

public interface IAppProcessor
{
	Task TryProcessAsync(string filePath);
}