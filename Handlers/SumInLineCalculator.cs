using CSVConsoleExplorer.Interfaces;
using System.Numerics;
using CSVConsoleExplorer.TextHandling.Components;
using CSVConsoleExplorer.TextHandling.Extensions;

namespace CSVConsoleExplorer.Handlers;

public class SumInLineCalculator : LineHandlerBase, ISumInLineCalculator
{
	
	private CsvLine _currentCsvLine = new(default, 0);
	private long _biggestSumInLines;
	private CsvLine _lineWithTheBiggestSum = new(default, 0);
	
	public override void SetCurrentLine(CsvLine currentCsvLine)
	{
		_currentCsvLine = currentCsvLine;
	}

	public CsvLine GetLineWithTheBiggestSum()
	{
		return _lineWithTheBiggestSum;
	}

	public long GetBiggestSumInLines()
	{
		return _biggestSumInLines;
	}

	protected override bool CanHandle()
	{
		return _currentCsvLine.IsNumerical();
	}
	
	protected override async Task Handle()
	{
		Task sumCalculationTask = Task.Run(() =>
		{
			if (_currentCsvLine.Elements != null)
			{
				var numbers = _currentCsvLine.Elements.ToBlockingEnumerable().Select(long.Parse);
				var currentSum = numbers.Sum();
		
				if (_biggestSumInLines < currentSum)
				{
					_biggestSumInLines = currentSum;

					_lineWithTheBiggestSum = _currentCsvLine;
				}
			}
		});
		await sumCalculationTask;
	}
}