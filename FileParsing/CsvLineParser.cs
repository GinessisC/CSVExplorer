using CsvConsoleExplorer.FileParsing.Components;
using CsvConsoleExplorer.Interfaces;

namespace CsvConsoleExplorer.FileParsing;
public class CsvLineParser : ICsvLineParser
{
	private readonly char _separator = ',';
	private readonly ISumInLineCalculator _sumInLineCalculator;
	private readonly IUnprocessedLineHandler _unprocessedLineHandler;
	private readonly ILinesReader _linesReader;
	
	public CsvLineParser(ISumInLineCalculator sumInLineCalculator,
		IUnprocessedLineHandler unprocessedLineHandler,
		ILinesReader linesReader)
	{
		_sumInLineCalculator = sumInLineCalculator;
		_unprocessedLineHandler = unprocessedLineHandler;
		_linesReader = linesReader;
	}
	
	public async Task<ParsedDataFromCsvFile> ParseCsvFile(string filePath)
	{
		_unprocessedLineHandler.SetHandler(_sumInLineCalculator);
		
		var i = 0;
		await foreach (string line in _linesReader.ReadLines(filePath))
		{
			i++;
			CsvLine currentLine = ConvertToCsvLine(line, i);
			await _unprocessedLineHandler.HandleLine(currentLine);
		} 
		return new ParsedDataFromCsvFile(_sumInLineCalculator, _unprocessedLineHandler);
	}
	private CsvLine ConvertToCsvLine(string line, int lineNumber)
	{
		var elements = line.Split(_separator).ToList();
		return new CsvLine(elements, lineNumber); 
	}
}