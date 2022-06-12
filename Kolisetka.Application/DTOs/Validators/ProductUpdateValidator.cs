using FluentValidation;
using Kolisetka.Application.DTOs.DtoProduct;
using Kolisetka.Application.Contracts.Persistence;

namespace Kolisetka.Application.DTOs.Validators
{
    class ProductUpdateValidator : AbstractValidator<ProductUpdateDto>
    {
        private readonly IProductRepository _productRepository;

        public ProductUpdateValidator(IProductRepository productRepository)
        {
            _productRepository = productRepository;

            RuleFor(prop => prop.Id)
                .GreaterThan(0)
                .MustAsync(async (id, token) =>
                {
                    var isProductExist = await _productRepository.IsExist(id);
                    return !isProductExist;
                }).WithMessage("{PropertyName} does not exist.");

            RuleFor(prop => prop.Category)
                .NotEmpty().WithMessage("{PropertyName} is required.")
                .NotNull().WithMessage("{PropertyName} is required.");

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
                .NotNull().WithMessage("{PropertyName} is required.");
        }
    }
}
