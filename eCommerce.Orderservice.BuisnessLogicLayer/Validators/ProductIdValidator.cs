using eCommerce.Orderservice.BuisnessLogicLayer.HttpClients;
using FluentValidation;

namespace eCommerce.Orderservice.BuisnessLogicLayer.Validators
{
    public static class ProductIdValidator
    {
        public static IRuleBuilderOptions<T, TProperty> ValidateProductID<T, TProperty>(
     this IRuleBuilder<T, TProperty> ruleBuilder,
     ProductMicroserviceClient productMicroserviceClient)
        {
            return ruleBuilder
                .NotEmpty().WithMessage("Product ID can't be blank")
                .MustAsync(async (productID, cancellation) =>
                {
                    // 1. Validate it's a valid Guid format firs
                    try
                    {
                        if (productID is null)
                        {
                            return false;
                        }
                        else
                        {
                            //Guid.TryParseExact(Guid.Parse(productID.ToString()).ToString(), "D", out Guid parsedGuid);
                            var product = await productMicroserviceClient.GetProductByProductID(Guid.Parse(productID.ToString()));
                            return product != null;
                        }

                    }
                    catch (Exception ex)
                    {
                        // Log error here if needed
                        return false;
                    }
                })
                .WithMessage("Product ID is invalid");
        }
    }
}
