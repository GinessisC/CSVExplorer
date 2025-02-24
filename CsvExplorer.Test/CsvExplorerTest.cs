using CsvConsoleExplorer.FileParsing;
using CsvConsoleExplorer.FileParsing.Components;
using CsvConsoleExplorer.FileParsing.Extensions;
using CsvConsoleExplorer.Handlers;
using CsvConsoleExplorer.Interfaces;
using NSubstitute;
using Xunit;

namespace CsvExplorer.Test;
public class TextHandlerTest
{
	const char Separator = ',';
	const string TestPath = "test.csv";
	
	[Theory]
	[MemberData(nameof(CorrectOnlyLines))]
	public async Task ParsingNumericalLinesOnly(IEnumerable<string> receivedLines)
	{
		//Arrange 
		SumInLineCalculator sumCalculator = new();
		CsvUnprocessedLineHandler unprocessedLineHandler = new();
		ILinesReader linesReader = Substitute.For<ILinesReader>();
		
		linesReader.ReadLines(TestPath).Returns(receivedLines.ToAsyncEnumerable());
		CsvLineParser parser = new(sumCalculator, unprocessedLineHandler, linesReader, Separator);
		
		//Act
		ParsedDataFromCsvFile data = await parser.ParseCsvFile(TestPath);
		var numbers = GetLineWithBiggestSum(data);
		
		//Assert
		Assert.Equal(11, numbers.Sum());
	}

	
	[Theory]
	[MemberData(nameof(UnprocessedLinesOnly))]
	public async Task ParsingUnprocessedLinesOnly(IEnumerable<string> receivedLines)
	{
		//Arrange 
		SumInLineCalculator sumCalculator = new();
		CsvUnprocessedLineHandler unprocessedLineHandler = new();
		ILinesReader linesReader = Substitute.For<ILinesReader>();
		
		linesReader.ReadLines(TestPath).Returns(receivedLines.ToAsyncEnumerable());
		CsvLineParser parser = new(sumCalculator, unprocessedLineHandler, linesReader, Separator);
		
		//Act
		ParsedDataFromCsvFile data = await parser.ParseCsvFile(TestPath);
		
		var unprocessedLines = data.GetUnprocessedLines();

		var areValid = await AreAllLinesValid(unprocessedLines);  
		
		//Assert
		Assert.True(areValid is false);
	}
		
	[Theory]
	[MemberData(nameof(RandomLines))]
	public async Task ParsingRandomLines(IEnumerable<string> receivedLines)
	{
		//Arrange
		SumInLineCalculator sumCalculator = new();
		CsvUnprocessedLineHandler unprocessedLineHandler = new();
		ILinesReader linesReader = Substitute.For<ILinesReader>();
		
		linesReader.ReadLines(TestPath).Returns(receivedLines.ToAsyncEnumerable());
		CsvLineParser parser = new(sumCalculator, unprocessedLineHandler, linesReader, Separator);
		
		//Act 
		ParsedDataFromCsvFile data = await parser.ParseCsvFile(TestPath);
		
		var lineWithBiggestSum = GetLineWithBiggestSum(data);
		CsvLine[] unprocessedLinesArray = await GetUnprocessedLinesAsync(data);
		//Assert
		
		Assert.Equal(15, lineWithBiggestSum.Sum());
		AssertUnprocessedLines(unprocessedLinesArray);
	}
	
	private IEnumerable<long> GetLineWithBiggestSum(ParsedDataFromCsvFile data)
	{
		var elementsInLine = data.GetLineWithBiggestSum().Elements;
		return elementsInLine.Select(long.Parse);
	}
	private async Task<CsvLine[]> GetUnprocessedLinesAsync(ParsedDataFromCsvFile data)
	{
		var unprocessedLines = data.GetUnprocessedLines();
		var unprocessedLinesArray = await unprocessedLines.ToArrayAsync();
		
		return unprocessedLinesArray;
	}
	private async Task<bool> AreAllLinesValid(IAsyncEnumerable<CsvLine> unprocessedLines)
	{
		var areElementsNumerical = unprocessedLines.AllAsync(line => line.IsNumerical()); //Why ValueTask? Bc we use here short-term operation that is completed sync?   
		return await areElementsNumerical;
	}
	private void AssertUnprocessedLines(CsvLine[] unprocessedLines)
	{
		Assert.Contains("", unprocessedLines[0].Elements);
		Assert.Contains("t", unprocessedLines[1].Elements);
	}
	public static IEnumerable<object[]> CorrectOnlyLines()
	{
		yield return
		[
			new[] {"1,2", "3,3", "5,6"}
		];
	}
	public static IEnumerable<object[]> UnprocessedLinesOnly()
	{
		yield return
		[
			new[] {"1,2,t", "1,2,", "1,,"}
		];
	}

	public static IEnumerable<object[]> RandomLines()
	{
		yield return
		[
			new[] {"1,2,3", "4,5,6", "1,,", "23,34,t"}
		];
	}
}