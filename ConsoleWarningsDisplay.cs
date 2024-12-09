using CSVConsoleExplorer.Interfaces;

namespace CSVConsoleExplorer;

public class ConsoleWarningsDisplay : IWarningsDisplay
{
	public void DisplayWarning(string warning)
	{
		Console.WriteLine(warning);
	}
}