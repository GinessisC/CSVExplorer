namespace CsvConsoleExplorer.Interfaces;

public interface IOutputDisplay
{
	Task DisplayParsedFileData(string filePath);
}