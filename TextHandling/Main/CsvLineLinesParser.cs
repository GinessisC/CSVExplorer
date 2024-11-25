using CSVConsoleExplorer.TextHandling;
using CSVConsoleExplorer.TextHandling.Components;

namespace CSVConsoleExplorer.TextHandling;

public class CsvLineLinesParser : CsvLineLinesHandlerBase
{
	private const string Separator = ";";
	private const string TempTxtFileName = "temp.txt";
	private static readonly string _currentDirectory = Directory.GetCurrentDirectory();
	private readonly string _fullPathToTempFile = Path.Combine(_currentDirectory, TempTxtFileName);
	private string PathToCsvFile { get; }
	
	public CsvLineLinesParser(string pathToCsvFile)
	{
		PathToCsvFile = pathToCsvFile;
	}
	
	protected override bool CanHandleLines()
	{
		return File.Exists(PathToCsvFile);
	}
	protected override async IAsyncEnumerable<CsvLine> GetHandledResult()
	{
		//CopyFileToProjectDirectory();
		await foreach (var line in File.ReadLinesAsync(_fullPathToTempFile))
		{
			var elements = line.Split(Separator).ToList();
			
			bool canBeHandled = elements.All(IsNumber);
			
			yield return new CsvLine(elements, canBeHandled);
		}
		//DeleteTempFile();
	}
	
	private void CopyFileToProjectDirectory()
	{
		File.Copy(PathToCsvFile, $"{_fullPathToTempFile}");
	}
	private void DeleteTempFile()
	{
		File.Delete(_fullPathToTempFile);
	}
	private bool IsNumber(string element)
	{
		return element.All(char.IsDigit);
	}
}