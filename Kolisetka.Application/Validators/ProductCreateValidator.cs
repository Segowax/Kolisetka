using FluentValidation;
using Kolisetka.Application.DTOs.DtoProduct;
using Kolisetka.Application.Properties;

namespace Kolisetka.Application.Validators
{
    class ProductCreateValidator : AbstractValidator<ProductCreateDto>
    {
        public ProductCreateValidator()
        {
            RuleFor(prop => prop.Category)
                .NotNull().WithMessage(Resources.Product_Validator_Required)
                .IsInEnum().WithMessage(Resources.Product_Validator_InvalidEnum);

            RuleFor(prop => prop.Description)
                .NotEmpty().WithMessage(Resources.Product_Validator_Required)
                .NotNull().WithMessage(Resources.Product_Validator_Required)
                .MaximumLength(1000).WithMessage(Resources.Product_Validator_TooLong);

            RuleFor(prop => prop.Name)
                .NotEmpty().WithMessage(Resources.Product_Validator_Required)
                .NotNull().WithMessage(Resources.Product_Validator_Required)
                .MaximumLength(100).WithMessage(Resources.Product_Validator_TooLong);

            RuleFor(prop => prop.Price)
                .NotEmpty().WithMessage(Resources.Product_Validator_Required)
                .NotNull().WithMessage(Resources.Product_Validator_Required)
                .GreaterThan(0).WithMessage(Resources.Product_Validator_GreaterThan0)
                .ScalePrecision(2, 7).WithMessage(Resources.Product_Validator_InvalidPrecision);
        }
    }
}
