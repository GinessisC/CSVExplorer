using CsvConsoleExplorer.Interfaces;
using CsvConsoleExplorer.TextHandling.Components;

namespace CsvConsoleExplorer.Handlers;

public abstract class LineHandlerBase : ILineHandler
{
	private ILineHandler? LineHandler { get; set; }
	public void SetHandler(ILineHandler? handler)
	{
		LineHandler = handler;
	}
	public async Task HandleLine(CsvLine line)
	{
		if (CanHandle(line))
		{
			await Handle(line);
		}

		else if (LineHandler != null)
		{
			await LineHandler.HandleLine(line);
		}
		else
		{
			throw new NullReferenceException();
		}
	}

	protected abstract bool CanHandle(CsvLine line);
	protected abstract Task Handle(CsvLine line);
}