namespace Product.Api.Models;

public record PagedResponse<T>
{
    public IEnumerable<T> Items { get; init; } = Array.Empty<T>();
    public int Total { get; init; }
    public int Page { get; init; }
    public int PageSize { get; init; }
}
