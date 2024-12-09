using CSVConsoleExplorer.Interfaces;
using CSVConsoleExplorer.TextHandling.Components;

namespace CSVConsoleExplorer.Handlers;

public abstract class LineHandlerBase : ILineHandler
{
	

	private ILineHandler? LineHandler { get; set; }
	//private IWarningsDisplay WarningsDisplay { get; set; }
	public void SetHandler(ILineHandler? handler)
	{
		LineHandler = handler;
	}
	public async Task HandleLine()
	{
		if (CanHandle())
		{
			await Handle();
		}

		else if (LineHandler != null)
		{
			await LineHandler.HandleLine();
		}
		else
		{
			throw new NullReferenceException();
		}
	}

	public abstract void SetCurrentLine(CsvLine currentCsvLine);
	protected abstract bool CanHandle();
	protected abstract Task Handle();
}