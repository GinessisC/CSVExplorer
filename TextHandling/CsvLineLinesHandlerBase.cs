using CSVConsoleExplorer.TextHandling.Components;
using CSVConsoleExplorer.Interfaces;

namespace CSVConsoleExplorer.TextHandling;

public abstract class CsvLineLinesHandlerBase : ILineHandler
{
	public virtual Task HandleLine()
	{
		if (CanHandleLines())
		{
			await FilterLines();
		}
	}

	protected abstract bool CanHandleLines();
	protected abstract void FilterLines();
}