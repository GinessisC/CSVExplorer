using CSVConsoleExplorer.TextHandling.Components;

namespace CSVConsoleExplorer.Interfaces;

public interface ILineHandler
{
	void SetHandler(ILineHandler? handler);
	Task HandleLine();
	void SetCurrentLine(CsvLine currentCsvLine);
}