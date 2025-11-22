namespace Product.Api.Models;

public record ApiResponse<T>
{
    public bool Success { get; init; } = true;
    public T? Data { get; init; }
    public string? Message { get; init; }
    public IDictionary<string, object?>? Meta { get; init; }
}
