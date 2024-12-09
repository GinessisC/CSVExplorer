using CSVConsoleExplorer.TextHandling.Components;

namespace CSVConsoleExplorer.Interfaces;

public interface IUnprocessedLineHandler : ILineHandler
{
	IAsyncEnumerable<CsvLine> GetUnprocessedCsvLines();
}