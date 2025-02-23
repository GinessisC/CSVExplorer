using CsvConsoleExplorer.FileParsing.Components;

namespace CsvConsoleExplorer.Interfaces;
public interface ISumInLineCalculator : ILineHandler
{
	CsvLine GetLineWithBiggestSum();
}