using CsvConsoleExplorer.Handlers;
using CsvConsoleExplorer.TextHandling.Components;

namespace CsvConsoleExplorer.Interfaces;

public interface ISumInLineCalculator : ILineHandler
{
	CsvLine GetLineWithTheBiggestSum();
}