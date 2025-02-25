using CsvConsoleExplorer.Interfaces;

namespace CsvConsoleExplorer;

public class AppProcessor : IAppProcessor
{
	private readonly IOutputDisplay _outputDisplay;

	public AppProcessor(IOutputDisplay outputDisplay)
	{
		_outputDisplay = outputDisplay;
	}

	public async Task TryProcessAsync(string filePath)
	{
		try
		{
			await ProcessAsync(filePath);
		}
		catch (Exception e)
		{
			Console.WriteLine(e);
		}
	}

	private async Task ProcessAsync(string filePath)
	{
		if (string.IsNullOrWhiteSpace(filePath) is false)
		{
			await _outputDisplay.DisplayParsedFileData(filePath);
		}
	}
}