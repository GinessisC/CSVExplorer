using CsvHandling.Interfaces.Parsing;

namespace CsvConsoleExplorer;
public class Parser : IFileParser
{
	private const char Separator  = ';';
	private readonly IEnumerable<string> _inputElements;
	
	public Parser(IEnumerable<string> inputElements)
	{
		_inputElements = inputElements;
	}
	
	private bool IsNumber(string element)
	{
		return element.All(char.IsDigit);
	}
	
	public IEnumerable<IEnumerable<int>> SortElements()
	{
		List<List<int>> linesOfNumbers = new();
		foreach (var line in _inputElements)
		{
			string[] elements = line.Split(Separator);
			List<int> numbers = new();
			foreach (var element in elements)
			{
				if (IsNumber(element) && !string.IsNullOrWhiteSpace(element))
				{
					numbers.Add(int.Parse(element));
				}
			}
			linesOfNumbers.Add(numbers);
		}
		return linesOfNumbers;
	}
}