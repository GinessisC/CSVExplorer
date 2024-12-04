using System.Diagnostics;
using System.Drawing;
using CSVConsoleExplorer.TextHandling;
using CSVConsoleExplorer.TextHandling.Components;

namespace CSVConsoleExplorer;

public static class ParseFactory
{
	public static CsvLineParser DefineParsers(FileSize size)
	{
		return size switch
		{
			FileSize.Big => new CsvLineParser(),
			FileSize.Small => new CsvLineParser(),
			_ => new CsvLineParser()
		};
	}
}