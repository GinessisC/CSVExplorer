using CsvHandling.Components;

namespace CsvHandling.Interfaces;

public interface IMessagesDisplayer
{
	void NotifyAboutUnprocessableLine(List<CsvLine> line);
}