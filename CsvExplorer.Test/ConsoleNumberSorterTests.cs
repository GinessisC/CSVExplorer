using System.ComponentModel;
using Xunit;

namespace CsvExplorer.Test;

public class ConsoleNumberSorterTests
{
	private readonly List<string> _linesOfSymbols = new()
	{
		"1;2;3;4;ff;symbol;s;!"
	};
	private readonly List<List<int>> _linesOfNumbers = new()
	{
		new List<int> {1, 2, 3, 4}
	};
	[Fact]
	[DisplayName("Inputs a list of symbols, outputs a list of numbers")]
	public void NumbersAreSorted()
	{
		
	}
}