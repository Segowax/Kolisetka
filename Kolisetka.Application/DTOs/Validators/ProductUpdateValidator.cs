using FluentValidation;
using Kolisetka.Application.DTOs.DtoProduct;
using Kolisetka.Application.Contracts.Persistence;
using Kolisetka.Application.Properties;

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
                    return isProductExist;
                }).WithMessage(Resources.Product_Validator_NotExists);

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
