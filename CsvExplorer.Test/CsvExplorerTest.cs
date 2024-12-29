using System.ComponentModel;
using CSVConsoleExplorer.Handlers;
using CSVConsoleExplorer.Interfaces;
using CSVConsoleExplorer.TextHandling;
using CSVConsoleExplorer.TextHandling.Components;
using CSVConsoleExplorer.TextHandling.Extensions;
using NSubstitute;
using Xunit;

namespace CsvExplorer.Test;

public class TextHandlerTest
{
	private static readonly List<string> _nonNumericElements = new()
	{
		"1", "2", "c", "d"
	};
	
	private static readonly CsvLine _nonNumericCsvLine = new(_nonNumericElements.ToAsyncEnumerable(),0);
	
	[Theory]
	[DisplayName("Checks for correct max sum value")]
	[InlineData(new[] {"1", "2", "9", "50"},
		new[] {"1", "2", "3", "4"},
		62)]
	public async Task CorrectMaxSumCalculation(string[] incorrectLine,
		string[] correctLine,
		long maxSumExpected)
	{
		//Arrange
		IWarningsDisplayer displayer = Substitute.For<IWarningsDisplayer>();
		
		CsvLine[] actualLines =
		[
			new(incorrectLine.ToAsyncEnumerable(), 0),
			new(correctLine.ToAsyncEnumerable(), 1)
		];
		
		SumInLineCalculator sumInLineCalculator = new(displayer);
		
		//Act
		foreach (CsvLine line in actualLines)
		{
			sumInLineCalculator.SetCurrentLine(line);
			await sumInLineCalculator.HandleLine();
		}
		
		//Assert
		Assert.Equal(maxSumExpected, sumInLineCalculator.BiggestSumInLines);
	}
	
	[Fact]
	[DisplayName("Checks for correct unprocessed lines handling")]
	public async Task UnprocessedLineHandling()
	{
		//Arrange
		IWarningsDisplayer displayer = Substitute.For<IWarningsDisplayer>();
		CsvUnprocessedLineHandler unprocessedLineHandler = new(displayer);
		
		//Act
		unprocessedLineHandler.SetCurrentLine(_nonNumericCsvLine);
		await unprocessedLineHandler.HandleLine();
		
		//Assert
		Assert.Contains(_nonNumericCsvLine, unprocessedLineHandler.UnprocessedCsvLines.ToBlockingEnumerable());
	}
	
}