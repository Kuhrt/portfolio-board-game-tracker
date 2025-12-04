using System.Collections;

namespace BoardGameTracker.Common.Models;

public class PagedList<T> : IReadOnlyList<T>
{
    private readonly IList<T> subset;
    public PagedList(IEnumerable<T> items, int count, int pageNumber, int pageSize)
    {
        PageNumber = pageNumber;
        subset = items as IList<T> ?? [.. items];
        TotalPages = subset.Count() == 0 ? 0 : (int)Math.Ceiling(count / (double)pageSize);
        Count = count;
    }
    public int PageNumber { get; }
    public int TotalPages { get; }
    public bool IsFirstPage => PageNumber == 1;
    public bool IsLastPage => PageNumber == TotalPages;
    public int Count { get; set; }
    public T this[int index] => subset[index];
    public IEnumerator<T> GetEnumerator() => subset.GetEnumerator();
    IEnumerator IEnumerable.GetEnumerator() => subset.GetEnumerator();
}