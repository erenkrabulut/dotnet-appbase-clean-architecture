namespace core.Application.Common.Paging
{
    public sealed class PageResponse<T>
    {
        public IReadOnlyList<T> Items { get; init; } = new List<T>();

        public int PageIndex { get; init; }
        public int PageSize { get; init; }
        public int TotalCount { get; init; }

        public int TotalPages =>
            PageSize <= 0 ? 0 : (TotalCount + PageSize - 1) / PageSize;

        public bool HasPrevious => PageIndex > 0;

        public bool HasNext => TotalPages > 0 && PageIndex + 1 < TotalPages;
    }
}
