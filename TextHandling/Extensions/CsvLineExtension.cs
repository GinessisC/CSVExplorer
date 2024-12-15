using CsvConsoleExplorer.TextHandling.Components;

namespace CsvConsoleExplorer.TextHandling.Extensions;
public static class CsvLineExtension
{
	public static bool IsNumerical(this CsvLine line) 
	{
		if (line.Elements == null)
		{
			return false;
		}
		var filteredLine = line.Elements.ToBlockingEnumerable()
			.Select(element => element)
			.Where(element => element != string.Empty);

		return filteredLine.All(IsNumber);
	}
	private static bool IsNumber(string element)
	{
		return element.All(char.IsDigit);
	}
}