using CSVConsoleExplorer.Handlers;
using CSVConsoleExplorer.TextHandling.Components;

namespace CSVConsoleExplorer.Interfaces;

public interface ISumInLineCalculator : ILineHandler
{
	CsvLine GetLineWithTheBiggestSum();
}