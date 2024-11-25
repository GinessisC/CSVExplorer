using CSVConsoleExplorer.TextHandling.Components;

namespace CSVConsoleExplorer.TextHandling;

public class NumericalLineHandler : CsvLineLinesHandlerBase
{
	private readonly IAsyncEnumerable<CsvLine?> _allCsvLines;

	public KeyValuePair<int, int> LineNumberMaxSumPair { get; private set; }
	
	public NumericalLineHandler(IAsyncEnumerable<CsvLine?> allCsvLines)
	{
		_allCsvLines = allCsvLines;
	}

	private async Task<KeyValuePair<int, int>> GetLineNumberMaxSumPair()
	{
		int maxSum = 0;
		int i = 0;
		int lineNumberWithMaxSum = 0;
		
		await foreach (var line in _allCsvLines)
		{
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

	protected override bool CanHandleLines()
	{
		return _allCsvLines.ToBlockingEnumerable().All(IsNumericalLine);
	}
	// private bool IsNumber(string str)
	// {
	// 	
	// 	//return int.TryParse(str, out _); - what does it mean?
	// }
	protected override async Task FilterLines()
	{
		LineNumberMaxSumPair = await GetLineNumberMaxSumPair();
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