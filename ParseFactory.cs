using System.Diagnostics;
using System.Drawing;
using CSVConsoleExplorer.TextHandling;
using CSVConsoleExplorer.TextHandling.Components;

namespace CSVConsoleExplorer;

public static class ParseFactory
{
	public static dynamic DefineParser(FileSize size)
	{
		return size switch
		{
			FileSize.Big => new CsvLineParser<long, long>(),
			FileSize.Small => new CsvLineParser<int, int>(),
			_ => new CsvLineParser<int, int>()
		};
	}
}