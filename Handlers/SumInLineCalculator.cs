using CSVConsoleExplorer.Interfaces;
using System.Numerics;
using CSVConsoleExplorer.TextHandling.Components;
using CSVConsoleExplorer.TextHandling.Extensions;

namespace CSVConsoleExplorer.Handlers;

public class SumInLineCalculator : LineHandlerBase, ISumInLineCalculator
{
	private long _biggestSumInLines;
	private CsvLine _lineWithTheBiggestSum = new(default, 0);
	
	public CsvLine GetLineWithTheBiggestSum()
	{
		return _lineWithTheBiggestSum;
	}

	public long GetBiggestSumInLines()
	{
		return _biggestSumInLines;
	}

	protected override bool CanHandle(CsvLine line)
	{
		return line.IsNumerical();
	}
	protected override async Task Handle(CsvLine line)
	{
		Task sumCalculationTask = Task.Run(() =>
		{
			if (line.Elements != null)
			{
				var numbers = line.Elements.ToBlockingEnumerable().Select(long.Parse);
				var currentSum = numbers.Sum();
		
				if (_biggestSumInLines < currentSum)
				{
					_biggestSumInLines = currentSum;

					_lineWithTheBiggestSum = line;
				}
			}
		});
		await sumCalculationTask;
	}
}