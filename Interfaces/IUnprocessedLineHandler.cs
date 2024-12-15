using CsvConsoleExplorer.TextHandling.Components;

namespace CsvConsoleExplorer.Interfaces;

public interface IUnprocessedLineHandler : ILineHandler
{
	IAsyncEnumerable<CsvLine> GetUnprocessedCsvLines();
}