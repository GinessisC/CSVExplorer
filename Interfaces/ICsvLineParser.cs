using CsvConsoleExplorer.FileParsing;

namespace CsvConsoleExplorer.Interfaces;
public interface ICsvLineParser
{
	Task<ParsedDataFromCsvFile> ParseCsvFile(string filePath);
}