using CsvConsoleExplorer.TextHandling;

namespace CsvConsoleExplorer.Interfaces;

public interface ICsvLineParser
{
	Task<ParsedDataFromCsvFile> ParseCsvFile(string filePath);
}