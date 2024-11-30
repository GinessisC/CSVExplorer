using CSVConsoleExplorer.TextHandling.Components;

namespace CSVConsoleExplorer.TextHandling;

public class NumericalLinesHandler
{
	private readonly IAsyncEnumerable<CsvLine?> _allCsvLines;
	
	public NumericalLinesHandler(IAsyncEnumerable<CsvLine> allCsvLines)
	{
		_allCsvLines = allCsvLines;
	}

	public async Task<KeyValuePair<int, int>> GetLineNumberMaxSumPairAsync()
	{
		int maxSum = 0;
		int i = 0;
		int lineNumberWithMaxSum = 0;
		
		await foreach (var line in _allCsvLines)
		{
			if (!IsNumericalLine(line))
			{
				continue;
			}
			i++;
			
			var numbers = line?.Elements.ToBlockingEnumerable().Select(int.Parse);
			int currentSum = numbers!.Sum();
			
			if (currentSum > maxSum)
			{
				lineNumberWithMaxSum = i;
				maxSum = currentSum;
			}
		}
		
		return new KeyValuePair<int, int>(lineNumberWithMaxSum, maxSum);
	}

	private bool IsNumericalLine(CsvLine? line)
	{
		var filteredLine = line?.Elements.ToBlockingEnumerable()
			.Select(element => element)
			.Where(element => element != string.Empty);

		return filteredLine != null && filteredLine.All(IsNumber);
	}
	
	private bool IsNumber(string element)
	{
		return element.All(char.IsDigit);
	}
}