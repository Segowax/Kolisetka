using FluentValidation;
using Kolisetka.Application.DTOs.DtoProduct;

namespace Kolisetka.Application.DTOs.Validators
{
    class ProductCreateValidator : AbstractValidator<ProductCreateDto>
    {
        public ProductCreateValidator()
        {
            RuleFor(prop => prop.Category)
                .NotNull().WithMessage("{PropertyName} is required.")
                .IsInEnum().WithMessage("{PropertyName} has to be included within \"Category\" enum.");

            RuleFor(prop => prop.Description)
                .NotEmpty().WithMessage("{PropertyName} is required.")
                .NotNull().WithMessage("{PropertyName} is required.")
                .MaximumLength(1000).WithMessage("{PropertyName} must not exceed {ComparisonValue} characters.");

            RuleFor(prop => prop.Name)
                .NotEmpty().WithMessage("{PropertyName} is required.")
                .NotNull().WithMessage("{PropertyName} is required.")
                .MaximumLength(100).WithMessage("{PropertyName} must not exceed {ComparisonValue} characters.");

            RuleFor(prop => prop.Price)
                .NotEmpty().WithMessage("{PropertyName} is required.")
                .NotNull().WithMessage("{PropertyName} is required.")
                .GreaterThan(0).WithMessage("{PropertyName} has to be grater than 0.")
                .ScalePrecision(2,7).WithMessage("{PropertyName} is decimal value with scale 2 and precision 7.");
        }
    }
}
