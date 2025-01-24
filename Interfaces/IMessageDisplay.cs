namespace CsvConsoleExplorer.Interfaces;

public interface IMessageDisplay
{
	Task ParseCsvFileAndDisplayData(string filePath);
}