using CSVConsoleExplorer.ConsoleInterfaceProviders;

Console.WriteLine("Welcome to CSV Console Explorer. Enter path to csv file:");
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