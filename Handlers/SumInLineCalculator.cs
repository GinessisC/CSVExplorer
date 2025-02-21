using System.Numerics;
using CsvConsoleExplorer.FileParsing.Components;
using CsvConsoleExplorer.FileParsing.Extensions;
using CsvConsoleExplorer.Interfaces;

namespace CsvConsoleExplorer.Handlers;
public class SumInLineCalculator : LineHandlerBase, ISumInLineCalculator
{
	private long _biggestSumInLines;
	private CsvLine _lineWithTheBiggestSum = new(default, 0);
	
	public CsvLine GetLineWithTheBiggestSum()
	{
		return _lineWithTheBiggestSum;
	}

	protected override bool CanHandle(CsvLine line)
	{
		return line.IsNumerical();
	}
	protected override async Task Handle(CsvLine line)
	{
		await Task.Run(() =>
		{
			var numbers = line.Elements.Select(long.Parse);
			var currentSum = numbers.Sum();
		
			if (_biggestSumInLines < currentSum)
			{
				_biggestSumInLines = currentSum;

				_lineWithTheBiggestSum = line;
			}
		});
	}
}