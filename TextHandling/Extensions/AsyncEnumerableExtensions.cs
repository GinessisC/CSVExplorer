using CSVConsoleExplorer.TextHandling.Components;

namespace CSVConsoleExplorer.TextHandling.Extensions;
public static class AsyncEnumerableExtensions
{
	public static async IAsyncEnumerable<T> Append<T>(this IAsyncEnumerable<T> source, T value)
	{
		await foreach (var item in source)
		{
			yield return item;
		}
		yield return value;
	}
	
}