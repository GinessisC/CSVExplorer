using CsvHandling.Components;
using CsvHandling.Interfaces;

namespace CsvHandling;

public abstract class CsvLineLinesHandlerBase : ICsvLineHandler
{
	private ICsvLineHandler? _handler;

	public void SetNextHandler(ICsvLineHandler? nextHandler)
	{
		_handler = nextHandler;
	}

	public virtual IAsyncEnumerable<CsvLine> HandleLines()
	{
		if (CanHandleLines())
		{
			return  GetHandledResult();
		}
		if (_handler != null)
		{
			return _handler.HandleLines();
		}
		throw new ArgumentException("Can't handle lines");
	}

	protected abstract bool CanHandleLines();
	protected abstract IAsyncEnumerable<CsvLine> GetHandledResult();
}