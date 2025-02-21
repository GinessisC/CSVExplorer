namespace CSVConsoleExplorer.TextHandling.Extensions;
public static class EnumerableExtension
{
	public static async IAsyncEnumerable<T> ToAsyncEnumerable<T>(this IEnumerable<T> enumerable)
	{
		using IEnumerator<T> enumerator = enumerable.GetEnumerator();

		while (await Task.Run(enumerator.MoveNext).ConfigureAwait(false))
		{
			yield return enumerator.Current;
		}
	}
}