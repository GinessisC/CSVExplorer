using System.ComponentModel;
using CsvHandling;
using CsvHandling.Interfaces.Sorting;
using NSubstitute;
using Xunit;

namespace CsvExplorer.Test;

public class TextHandlerTest
{
	private const int MaxSumExpected = 117;
	private readonly List<List<int>> _linesOfNumbers = new()
	{
		new List<int> {1, 2, 3, 4},
		new List<int> {100, 4, 6, 7}
	};
	
	[Fact]
	[DisplayName("Checks for correct max sum value")]
	public void CorrectSumSelection()
	{
		//Arrange
		IFileNumberSorter fileNumberSorter = Substitute.For<IFileNumberSorter>();
		fileNumberSorter.GetNumbersOnly().Returns(_linesOfNumbers);
		SumCounter lh = new(fileNumberSorter);
		//Act

		var maxSum = lh.GetMaxSumInLines();
		
		//Assert
		fileNumberSorter.Received(1).GetNumbersOnly();
		Assert.Equal(MaxSumExpected, maxSum);
	}
}