using eCommerce.Orderservice.BuisnessLogicLayer.DTO;
using eCommerce.Orderservice.BuisnessLogicLayer.HttpClients;
using FluentValidation;
namespace eCommerce.Orderservice.BuisnessLogicLayer.Validators;

public class OrderItemAddRequestValidator : AbstractValidator<OrderItemAddRequest>
{
  public OrderItemAddRequestValidator(ProductMicroserviceClient productMicroserviceClient)
  {
        //ProductID
        RuleFor(temp => temp.ProductID).ValidateProductID(productMicroserviceClient);


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
