using CsvHandling.Components;

namespace CsvHandling.Interfaces;

public interface ICsvLineHandler
{
	void SetNextHandler(ICsvLineHandler? nextHandler);
	IAsyncEnumerable<CsvLine> HandleLines();
}