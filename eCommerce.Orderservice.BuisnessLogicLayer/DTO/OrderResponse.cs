namespace eCommerce.Orderservice.BuisnessLogicLayer.DTO;

public record OrderResponse(Guid OrderID, Guid UserID, decimal TotalBill, DateTime OrderDate,string? UserPersonName,string? UserEmail, List<OrderItemResponse> OrderItems)
{
  public OrderResponse() : this(default, default, default, default, default, default, default)
  {
  }
}

