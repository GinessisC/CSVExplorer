using System.Collections;
using System.ComponentModel;
using CsvConsoleExplorer.Handlers;
using CsvConsoleExplorer.Interfaces;
using CsvConsoleExplorer.TextHandling;
using CsvConsoleExplorer.TextHandling.Components;
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
		var numbers = parsedData.GetLineWithBiggestSum().Elements
			.Select(int.Parse);
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
				result.Add(line.Elements.ToList().Contains(element));
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

	private static async IAsyncEnumerable<string[]> GetEmptyEnumerable()
	{
		await Task.CompletedTask;
		yield break;
	}
}