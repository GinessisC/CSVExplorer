using CSVConsoleExplorer.TextHandling;
using CSVConsoleExplorer.TextHandling.Components;
using CSVConsoleExplorer.TextHandling.Extensions;

namespace CSVConsoleExplorer.TextHandling;

public static class CsvLineLinesParser
{
	private const string Separator = ",";
	
	public static async IAsyncEnumerable<CsvLine> ParseCsvFile(string csvFilePath)
	{
		await foreach (var line in File.ReadLinesAsync(csvFilePath))
		{
			var elements = line.Split(Separator).ToList();
			IEnumerable<string> filteredLine = DeleteUnnecessaryElements(elements);
			IAsyncEnumerable<string> filteredLineAsync = filteredLine.ToAsyncEnumerable();
			
			yield return new CsvLine(filteredLineAsync);
		}
	}
	private static IEnumerable<string> DeleteUnnecessaryElements(List<string> elements)
	{
		IEnumerable<string> filteredLine = elements
			.Select(element => element)
			.Where(element => element != string.Empty);

		return filteredLine;
	}
}