using CSVConsoleExplorer.TextHandling.Components;

namespace CSVConsoleExplorer.TextHandling.Interfaces;

public interface IMessagesDisplayer
{
	void NotifyAboutUnprocessableLine(List<CsvLine> line);
}