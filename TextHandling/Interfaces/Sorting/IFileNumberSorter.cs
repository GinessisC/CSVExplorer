namespace CsvHandling.Interfaces.Sorting;

public interface IFileNumberSorter
{
	IEnumerable<IEnumerable<int>> GetNumbersOnly();
}