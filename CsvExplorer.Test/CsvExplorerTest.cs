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
	
	[Theory]
	[DisplayName("Checks for correct unprocessed lines handling")]
	[InlineData(new[] {"1", "2", "9", "50"},
		new[] {"1", "test", "???", "a"})]
	public async Task UnprocessedLineHandling(
		string[] processedElements,
		string[] unprocessedElements)
	{
		//Arrange
		IWarningsDisplayer displayer = Substitute.For<IWarningsDisplayer>();
		CsvUnprocessedLineHandler unprocessedLineHandler = new(displayer);
		CsvLine unprocessedLine = new(unprocessedElements.ToAsyncEnumerable(), 0);
		CsvLine processedLine = new(processedElements.ToAsyncEnumerable(), 0);
		CsvLine[] lines = 
		{
			unprocessedLine,
			processedLine
		};
		foreach (CsvLine line in lines)
		{
			unprocessedLineHandler.SetCurrentLine(line);
			await unprocessedLineHandler.HandleLine();
		}
		//Assert
		Assert.Contains(unprocessedLine, unprocessedLineHandler.UnprocessedCsvLines.ToBlockingEnumerable());
	}
	
}