using CSVConsoleExplorer.Interfaces;

namespace CSVConsoleExplorer.Handlers;

public abstract class LineHandlerBase : ILineHandler
{
	protected LineHandlerBase(IWarningsDisplayer warningsDisplayer)
	{
		WarningsDisplayer = warningsDisplayer;
	}

	private ILineHandler? LineHandler { get; set; }
	private IWarningsDisplayer WarningsDisplayer { get; set; }
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
			WarningsDisplayer.DisplayWarning("Can't handle line");
		}
	}

	protected abstract bool CanHandle();
	protected abstract Task Handle();
}