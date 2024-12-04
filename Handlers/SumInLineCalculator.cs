using CSVConsoleExplorer.Interfaces;
using System.Numerics;
using CSVConsoleExplorer.TextHandling.Components;
using CSVConsoleExplorer.TextHandling.Extensions;

namespace CSVConsoleExplorer.Handlers;

public class SumInLineCalculator : LineHandlerBase
{
	public SumInLineCalculator(IWarningsDisplayer warningsDisplayer) : base(warningsDisplayer)
	{
		
	}

	private CsvLine CurrentCsvLine { get; set; } = new(default, 0);
	public long BiggestSumInLines { get; private set; }
	public CsvLine LineWithTheBiggestSum {get; private set;} = new(default, 0);
	
	public void SetCurrentLine(CsvLine currentCsvLine)
	{
		CurrentCsvLine = currentCsvLine;
	}
	protected override bool CanHandle()
	{
		return CurrentCsvLine.IsNumerical();
	}
	
	protected override async Task Handle()
	{
		Task sumCalculationTask = Task.Run(() =>
		{
			if (CurrentCsvLine.Elements != null)
			{
				var numbers = CurrentCsvLine.Elements.ToBlockingEnumerable().Select(long.Parse);
				var currentSum = numbers.Sum();
		
				if (BiggestSumInLines < currentSum)
				{
					BiggestSumInLines = currentSum;

					LineWithTheBiggestSum = CurrentCsvLine;
				}
			}
		});
		await sumCalculationTask;
	}
}