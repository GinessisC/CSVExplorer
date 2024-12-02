using System.Runtime.Intrinsics.Arm;
using CSVConsoleExplorer.TextHandling;
using CSVConsoleExplorer.TextHandling.Components;
using Cocona;
using CSVConsoleExplorer.ConsoleInterfaceProviders;

namespace CSVConsoleExplorer;
class Program
{
	public static async Task Main(string[] args)
	{
		string? path = Console.ReadLine();

		if (string.IsNullOrEmpty(path))
		{
			Console.WriteLine("Please specify a path to a CSV file");
			return;
		}
		if (args.Length > 0)
		{
			ConsoleInterfaceWithArgumentsProvider consoleInterface = new(path);
			await consoleInterface.RunAsync();
			return;
		}
		
		ConsoleInterfaceProvider interfaceProvider = new(path);
		await interfaceProvider.RunAsync();
	}
}