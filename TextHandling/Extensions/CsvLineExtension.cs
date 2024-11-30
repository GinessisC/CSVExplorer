using System.Numerics;
using CSVConsoleExplorer.TextHandling.Components;

namespace CSVConsoleExplorer.TextHandling.Extensions;

public static class CsvLineExtension
{
	public static bool IsNumerical<TLineNumber>(this CsvLine<TLineNumber> line) 
		where TLineNumber : INumber<TLineNumber>
	{
		var filteredLine = line.Elements.ToBlockingEnumerable()
			.Select(element => element)
			.Where(element => element != string.Empty);

		return  filteredLine.All(IsNumber);
	}
	

	private static bool IsNumber(string element)
	{
		return element.All(char.IsDigit);
	}
}