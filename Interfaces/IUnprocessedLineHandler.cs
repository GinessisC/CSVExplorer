using CsvConsoleExplorer.FileParsing.Components;

namespace CsvConsoleExplorer.Interfaces;
public interface IUnprocessedLineHandler : ILineHandler
{
	IAsyncEnumerable<CsvLine> GetUnprocessedLines();
}