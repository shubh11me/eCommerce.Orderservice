namespace BuisnessLogicLayer.DTO
{
    public record ProductResponse(
        Guid ProductId,
        string ProductName,
        string? Category,
        decimal? UnitPrice,
        int? QuantityInStock
    )
    {
        public ProductResponse()
            : this(default, default, default, default, default)
        {
        }
    }
}
