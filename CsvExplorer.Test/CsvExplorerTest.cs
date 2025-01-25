using System.Collections;
using System.ComponentModel;
using CsvConsoleExplorer.Handlers;
using CsvConsoleExplorer.Interfaces;
using CsvConsoleExplorer.TextHandling;
using CsvConsoleExplorer.TextHandling.Components;
using CsvConsoleExplorer.TextHandling.Extensions;
using NSubstitute;
using Xunit;

namespace CsvExplorer.Test;

public class TestDataGenerator : IEnumerable<object[]>
{
	const char Separator = ',';
	private readonly List<object[]> _data = new()
	{
		new object[] {5, 1, 3, 9},
		new object[] {7, 1, 5, 3}
	};
	public IEnumerator<object[]> GetEnumerator() => _data.GetEnumerator();
	IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
}
public class TextHandlerTest
{
	const char Separator = ',';
	const string TestPath = "test.csv";
	[Theory]
	[DisplayName("Checks for correct parsed lines")]
	[MemberData(nameof(GetCorrectOnlyLines))]
	public async Task ParsingNumericalLinesOnlyCorrectly(IEnumerable<string> receivedLines)
	{
		//Arrange 
		SumInLineCalculator sumInLineCalculator = new();
		CsvUnprocessedLineHandler unprocessedLineHandler = new();
		ILinesReceiver linesReceiver = Substitute.For<ILinesReceiver>();
		
		linesReceiver.ReadLines(TestPath).Returns(receivedLines.ToAsyncEnumerable());
		CsvLineParser parser = new(sumInLineCalculator, unprocessedLineHandler, linesReceiver, Separator);
		
		//Act
		ParsedDataFromCsvFile parsedData = await parser.ParseCsvFile(TestPath);
		var numbers = parsedData.GetLineWithBiggestSum().Elements
			.Select(long.Parse);
		
		//Assert
		Assert.Equal(11, numbers.Sum());
	}
	[Theory]
	[DisplayName("Checks for correct parsed lines")]
	[MemberData(nameof(GetIncorrectOnlyLines))]
	public async Task ParsingUnprocessedLinesOnly(IEnumerable<string> receivedLines)
	{
		//Arrange 
		SumInLineCalculator sumInLineCalculator = new();
		CsvUnprocessedLineHandler unprocessedLineHandler = new();
		ILinesReceiver linesReceiver = Substitute.For<ILinesReceiver>();
		
		linesReceiver.ReadLines(TestPath).Returns(receivedLines.ToAsyncEnumerable());
		CsvLineParser parser = new(sumInLineCalculator, unprocessedLineHandler, linesReceiver, ',');
		
		//Act
		ParsedDataFromCsvFile parsedData = await parser.ParseCsvFile(TestPath);
		var unprocessedLines = parsedData.GetUnprocessedLines();

		var isElementsNumerical = unprocessedLines.AllAsync(line => line.IsNumerical());
		
		//Assert
		Assert.False(await isElementsNumerical);
	}

	[Theory]
	[MemberData(nameof(GetIncorrectAndCorrectLines))]
	public async Task ParsingIncorrectAndCorrectLines(IEnumerable<string> receivedLines)
	{
		//Arrange
		SumInLineCalculator calculator = new();
		CsvUnprocessedLineHandler unprocessedLineHandler = new();
		ILinesReceiver linesReceiver = Substitute.For<ILinesReceiver>();
		linesReceiver.ReadLines(TestPath).Returns(receivedLines.ToAsyncEnumerable());
		
		CsvLineParser parser = new(calculator, unprocessedLineHandler, linesReceiver, Separator);
		
		//Act 
		var data = await parser.ParseCsvFile(TestPath);
		
		//Assert
		var unprocessedLines = data.GetUnprocessedLines();
		var lineWithBiggestSum = data.GetLineWithBiggestSum().Elements.Select(long.Parse);
		
		Assert.Equal(15, lineWithBiggestSum.Sum());
		var unprocessedLinesArray = await unprocessedLines.ToArrayAsync();
		Assert.Contains("", unprocessedLinesArray[0].Elements);
		Assert.Contains("t", unprocessedLinesArray[1].Elements);
		
		
	}
	public static IEnumerable<object[]> GetCorrectOnlyLines()
	{
		yield return new object[]
		{
			new[] {"1,2", "3,3", "5,6"},
		};
	}
	public static IEnumerable<object[]> GetIncorrectOnlyLines()
	{
		yield return new object[]
		{
			new[] {"1,2,t", "1,2,", "1,,"}
		};
	}

	public static IEnumerable<object[]> GetIncorrectAndCorrectLines()
	{
		yield return new object[]
		{
			new[] {"1,2,3", "4,5,6", "1,,", "23,34,t"}
		};
	}
}