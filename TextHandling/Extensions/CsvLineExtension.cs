using CsvConsoleExplorer.TextHandling.Components;

namespace CsvConsoleExplorer.TextHandling.Extensions;
public static class CsvLineExtension
{
	public static bool IsNumerical(this CsvLine line) 
	{
		var elements = line.Elements;
		var isElementsNumerical = elements.All(element => !string.IsNullOrEmpty(element) && IsNumber(element));
		
		return isElementsNumerical;
	}
	private static bool IsNumber(string element)
	{
		return element.All(char.IsDigit);
	}
}