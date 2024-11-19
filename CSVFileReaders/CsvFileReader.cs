namespace CSVFileReaders;

public class CsvFileReader
{
	private static readonly string _tempTxtFileName = "temp.txt";
	private static readonly string _currentDirectory = Directory.GetCurrentDirectory();
	private readonly string _fullPathToTempFile = Path.Combine(_currentDirectory, _tempTxtFileName);
	private string PathToCsvFile { get; }
	
	public CsvFileReader(string pathToCsvFile)
	{
		PathToCsvFile = pathToCsvFile;
	}
	public async Task<string[]> GetFileLinesAsync()
	{
		CopyFileToProjectDirectory();
	 	var lines = await File.ReadAllLinesAsync(_fullPathToTempFile);
		DeleteTempFile();
		return lines;
	}
	private void CopyFileToProjectDirectory()
	{
		File.Copy(PathToCsvFile, $"{_fullPathToTempFile}");
	}
	private void DeleteTempFile()
	{
		File.Delete(_fullPathToTempFile);
	}
}