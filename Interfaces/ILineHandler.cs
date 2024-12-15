using CsvConsoleExplorer.TextHandling.Components;

namespace CsvConsoleExplorer.Interfaces;

public interface ILineHandler
{
	void SetHandler(ILineHandler? handler);
	Task HandleLine(CsvLine line);
}