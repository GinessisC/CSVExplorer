using CsvConsoleExplorer.Interfaces;
using CsvConsoleExplorer.TextHandling.Components;

namespace CsvConsoleExplorer.Handlers;

public abstract class LineHandlerBase : ILineHandler
{
	private ILineHandler? _lineHandler;
	public void SetHandler(ILineHandler? handler)
	{
		_lineHandler = handler;
	}
	public async Task HandleLine(CsvLine line)
	{
		if (CanHandle(line))
		{
			await Handle(line);
		}

		else if (_lineHandler != null)
		{
			await _lineHandler.HandleLine(line);
		}
		else
		{
			throw new NullReferenceException();
		}
	}

	protected abstract bool CanHandle(CsvLine line);
	protected abstract Task Handle(CsvLine line);
}