using CsvHandling.Components;

namespace CsvHandling;

public class SumInLineCounter 
{
	private readonly IAsyncEnumerable<CsvLine?> _csvLines;
	public int IndexOfLineWithBiggestSum { get; private set; }
	public SumInLineCounter(IAsyncEnumerable<CsvLine?> csvLines)
	{
		_csvLines = csvLines;
	}

	public async Task<int> GetBiggestSumAsync()
	{
		int maxSum = 0;
		int i = 0;
		
		await foreach (var line in _csvLines)
		{
			i++;
			
			if (!line!.CanBeHandled)
			{
				continue;
			}
			
			var numbers = line?.Elements.Select(int.Parse);
			int currentSum = numbers!.Sum();
				
			if (currentSum > maxSum)
			{
				IndexOfLineWithBiggestSum = i;
				maxSum = currentSum;
			}
		}
		
		return maxSum;
	}
}