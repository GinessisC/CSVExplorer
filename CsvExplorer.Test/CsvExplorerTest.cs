using System.Collections;
using System.ComponentModel;
using CSVConsoleExplorer.Handlers;
using CSVConsoleExplorer.Interfaces;
using CSVConsoleExplorer.TextHandling;
using CSVConsoleExplorer.TextHandling.Components;
using CSVConsoleExplorer.TextHandling.Extensions;
using CSVConsoleExplorer.TextHandling.Extensions;
using NSubstitute;
using Xunit;

namespace CsvExplorer.Test;

public class TestDataGenerator : IEnumerable<object[]>
{
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
	private const string TestPath = "C:\\Test\\test.csv";
	
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
		
		CsvLine[] actualLines =
		[
			new(incorrectLine.ToAsyncEnumerable(), 0),
			new(correctLine.ToAsyncEnumerable(), 1)
		];
		
		SumInLineCalculator sumInLineCalculator = new();
		
		//Act
		foreach (CsvLine line in actualLines)
		{
		}
		
		//Assert
		Assert.Equal(maxSumExpected, sumInLineCalculator.GetBiggestSumInLines());
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
		CsvUnprocessedLineHandler unprocessedLineHandler = new();
		CsvLine unprocessedLine = new(unprocessedElements.ToAsyncEnumerable(), 0);
		CsvLine processedLine = new(processedElements.ToAsyncEnumerable(), 0);
		
		CsvLine[] lines = 
		{
			unprocessedLine,
			processedLine
		};
		//Act
		foreach (CsvLine line in lines)
		{
			await unprocessedLineHandler.HandleLine(line);
		}
		//Assert
		Assert.Contains(unprocessedLine, unprocessedLineHandler.GetUnprocessedCsvLines().ToBlockingEnumerable());
	}

	
	[Theory]
	[DisplayName("Checks for correct parsed lines")]
	[MemberData(nameof(GetCorrectOnlyDataForParsing))]
	public async Task ParsingNumericalLinesOnlyCorrectly(IEnumerable<string> receivedLines)
	{
		//Arrange 
		SumInLineCalculator sumInLineCalculator = new();
		CsvUnprocessedLineHandler unprocessedLineHandler = new();
		ILinesReceiver linesReceiver = Substitute.For<ILinesReceiver>();
		IAsyncEnumerable<string[]> lines = GetEmptyEnumerable();
		
		linesReceiver.ReadLines(TestPath).Returns(receivedLines.ToAsyncEnumerable());
		CsvLineParser parser = new(sumInLineCalculator, unprocessedLineHandler, linesReceiver, ',');
		
		//Act
		
		ParsedDataFromCsvFile parsedData = await parser.ParseCsvFile(TestPath);
		var numbers = parsedData.GetLineWithBiggestSum().Elements.ToBlockingEnumerable()
			.Select(x => int.Parse(x));
		//Assert
		Assert.Equal(11, numbers.Sum());
	}
	[Theory]
	[DisplayName("Checks for correct parsed lines")]
	[MemberData(nameof(GetIncorrectOnlyLineForParsing))]
	public async Task ParsingUnprocessedLinesOnly(string receivedLine)
	{
		//Arrange 
		SumInLineCalculator sumInLineCalculator = new();
		CsvUnprocessedLineHandler unprocessedLineHandler = new();
		ILinesReceiver linesReceiver = Substitute.For<ILinesReceiver>();

		string[] arrayWithUnprocessedLine = new[]
		{
			receivedLine
		};
		
		linesReceiver.ReadLines(TestPath).Returns(arrayWithUnprocessedLine.ToAsyncEnumerable());
		CsvLineParser parser = new(sumInLineCalculator, unprocessedLineHandler, linesReceiver, ',');
		
		//Act
		
		
		
		ParsedDataFromCsvFile parsedData = await parser.ParseCsvFile(TestPath);
		IEnumerable<CsvLine> unprocessedLines = parsedData.GetUnprocessedLines().ToBlockingEnumerable();
		//Assert
		List<bool> result = new();

		List<string> elements = receivedLine.Split(',').ToList();

		foreach (string element in elements)
		{
			foreach(CsvLine line in unprocessedLines)
			{
				result.Add(line.Elements.ToBlockingEnumerable().ToList().Contains(element));
			}
		}

		Assert.True(result.All(x => x));
	}
	public static IEnumerable<object[]> GetCorrectOnlyDataForParsing()
	{
		yield return new object[]
		{
			new[] {"1,2,3", "3,4", "5,6"},
		};
	}
	public static IEnumerable<object[]> GetIncorrectOnlyLineForParsing()
	{
		yield return new object[]
		{
			"test,abc,t"
		};
	}
	public static async IAsyncEnumerable<string[]> GetEmptyEnumerable()
	{
		await Task.CompletedTask;
		yield break;
	}
}