using CsvHandling.Components;
using CsvHandling.Interfaces.Parsing;

namespace CsvHandling;
public class LineLinesStatistics : CsvLineLinesHandlerBase
{
	private readonly IFileParser _fileParser;
	public int MaxSumLine { get; private set; }
	public LineLinesStatistics(IFileParser fileParser)
	{
		_fileParser = fileParser;
	}
	
	public int GetMaxSumInLines()
	{
		var linesOfnumbers = _fileParser.SortElements();
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
	
	protected override bool CanHandleLines(CsvFile csvFile)
	{
		return true;
	}

	protected override bool CanHandleLines(IEnumerable<CsvLine> lines)
	{
		throw new NotImplementedException();
	}

	protected override IAsyncEnumerable<CsvLine> GetHandledResult()
	{
		
	}
}