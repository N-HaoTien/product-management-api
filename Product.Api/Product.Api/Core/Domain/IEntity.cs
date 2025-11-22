namespace Product.Api.Core.Domain;

public abstract class EntityBase<T>
{
    // Ensure Id is initialized to satisfy nullable analysis for reference generic types
    public T Id { get; set; } = default!;
}