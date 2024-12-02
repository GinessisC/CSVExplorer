using CSVConsoleExplorer.Interfaces;
using System.Numerics;
using CSVConsoleExplorer.TextHandling.Components;
using CSVConsoleExplorer.TextHandling.Extensions;

namespace CSVConsoleExplorer.Handlers;

public class SumInLineCalculator<TLineNumber, TSum> : LineHandlerBase
	where TLineNumber : INumber<TLineNumber>
	where TSum : INumber<TSum>
{
	private CsvLine<TLineNumber> CurrentCsvLine { get; set; } = new(default, TLineNumber.Zero);
	public TSum BiggestSumInLines { get; private set; } = TSum.Zero;
	public CsvLine<TLineNumber> LineWithTheBiggestSum {get; private set;} = new(default, TLineNumber.Zero);
	
	public void SetCurrentLine(CsvLine<TLineNumber> currentCsvLine)
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
			var numbers = CurrentCsvLine.Elements.ToBlockingEnumerable().Select(int.Parse);
			var currentSum = numbers.Sum();
			var parsedSum = TSum.Parse(currentSum.ToString(), null);
		
			if (BiggestSumInLines < parsedSum)
			{
				BiggestSumInLines = parsedSum;

				LineWithTheBiggestSum = CurrentCsvLine;
			}
		});
		await sumCalculationTask;
	}
	
	
}