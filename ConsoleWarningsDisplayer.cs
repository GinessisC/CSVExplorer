using CSVConsoleExplorer.Interfaces;

namespace CSVConsoleExplorer;

public class ConsoleWarningsDisplayer : IWarningsDisplayer
{
	public void DisplayWarning(string warning)
	{
		Console.WriteLine(warning);
	}
}