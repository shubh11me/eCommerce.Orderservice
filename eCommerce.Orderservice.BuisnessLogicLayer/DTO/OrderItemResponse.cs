namespace eCommerce.Orderservice.BuisnessLogicLayer.DTO;

public record OrderItemResponse(Guid ProductID, decimal UnitPrice, int Quantity, decimal TotalPrice,string ProductName,string ProductCategory)
{
  public OrderItemResponse() : this(default, default, default, default,default,default)
  {
  }
}
