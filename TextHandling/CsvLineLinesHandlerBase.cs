using CSVConsoleExplorer.TextHandling.Components;
using CSVConsoleExplorer.TextHandling.Interfaces;

namespace CSVConsoleExplorer.TextHandling;

public abstract class CsvLineLinesHandlerBase : ICsvLineHandler
{
	public virtual void HandleLines()
	{
		if (CanHandleLines())
		{
			FilterLines();
		}
	}

	protected abstract bool CanHandleLines();
	protected abstract void FilterLines();
}