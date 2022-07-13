using FluentValidation;
using Kolisetka.Application.DTOs.DtoProduct;
using Kolisetka.Application.Contracts.Persistence;
using Kolisetka.Application.Properties;

namespace Kolisetka.Application.Validators
{
    public class ProductDeleteValidator : AbstractValidator<ProductDeleteDto>
    {
        private readonly IProductRepository _productRepository;

        public ProductDeleteValidator(IProductRepository productRepository)
        {
            _productRepository = productRepository;

            RuleFor(prop => prop.Id)
                .GreaterThan(0)
                .MustAsync(async (id, token) =>
                {
                    return await _productRepository.IsExist(id);
                }).WithMessage(Resources.Product_Validator_NotExists);
        }
    }
}
