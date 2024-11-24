namespace CsvHandling.Components;

public class CsvFile
{
	private readonly string _filePath;

	// public async Task<List<CsvLine>> GetLinesAsync()
	// {
	// 	List<CsvLine> lines = new();
	// 	await foreach (var item in File.ReadLinesAsync(_filePath))
	// 	{
	// 		
	// 	}
	// 	return new CsvLine();
	// }
	public CsvFile(string filePath)
	{
		_filePath = filePath;
	}
	public bool Exists()
	{
		return File.Exists(_filePath);
	}
	
}