namespace CsvHandling.Components;

public class CsvLine
{
	public List<string> Elements { get; private set; }
	public bool CanBeHandled { get; init; }
	
	public CsvLine(List<string> elements, bool canBeHandled)
	{
		Elements = elements;
		CanBeHandled = canBeHandled;
	}
}