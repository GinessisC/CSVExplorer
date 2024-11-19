using CsvHandling.Interfaces.Sorting;

namespace CsvHandling;
public class SumCounter
{
	private readonly IFileNumberSorter _fileNumberSorter;
	public int MaxSumLine { get; private set; } = 0;
	public SumCounter(IFileNumberSorter fileNumberSorter)
	{
		_fileNumberSorter = fileNumberSorter;
	}

	public int GetMaxSumInLines()
	{
		var linesOfnumbers = _fileNumberSorter.GetNumbersOnly();
		int maxSum = 0;
		int i = 0;
		
		foreach (var line in linesOfnumbers)
		{
			i++;
			int currentSum = line.Sum();

			if (currentSum > maxSum)
			{
				MaxSumLine = i;
				maxSum = currentSum;
			}
		}
		return maxSum;
	}
}