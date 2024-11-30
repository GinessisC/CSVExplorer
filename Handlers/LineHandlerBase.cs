using CSVConsoleExplorer.Interfaces;

namespace CSVConsoleExplorer.Handlers;

public abstract class LineHandlerBase : ILineHandler
{
	private ILineHandler? LineHandler { get; set; }
	
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
			throw new Exception("Can't handle line");
		}
	}

	protected abstract bool CanHandle();
	protected abstract Task Handle();
}