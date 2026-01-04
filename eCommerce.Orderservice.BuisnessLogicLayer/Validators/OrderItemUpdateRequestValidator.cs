using eCommerce.Orderservice.BuisnessLogicLayer.DTO;
using eCommerce.Orderservice.BuisnessLogicLayer.HttpClients;
using FluentValidation;

namespace eCommerce.Orderservice.BuisnessLogicLayer.Validators;

public class OrderItemUpdateRequestValidator : AbstractValidator<OrderItemUpdateRequest>
{
  public OrderItemUpdateRequestValidator(ProductMicroserviceClient microserviceClient)
  {
    //ProductID
    RuleFor(temp => temp.ProductID)
      .ValidateProductID(microserviceClient);

    //UnitPrice
    RuleFor(temp => temp.UnitPrice)
      .NotEmpty().WithErrorCode("Unit Price can't be blank")
      .GreaterThan(0).WithErrorCode("Unit Price can't be less than or equal to zero");

    //Quantity
    RuleFor(temp => temp.Quantity)
      .NotEmpty().WithErrorCode("Quantity can't be blank")
      .GreaterThan(0).WithErrorCode("Quantity can't be less than or equal to zero");
  }
}
