using AutoMapper;
using Kolisetka.Application.DTOs.Validators;
using Kolisetka.Application.Features.Products.Requests.Commands;
using Kolisetka.Application.Contracts.Persistence;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using Kolisetka.Application.Responses;
using System.Linq;

namespace Kolisetka.Application.Features.Products.Handlers.Commands
{
    public class UpdateProductCommandHandler : IRequestHandler<UpdateProductCommand, BaseCommandResponse>
    {
        private readonly IProductRepository _productRepository;
        private readonly IMapper _mapper;

        public UpdateProductCommandHandler(IProductRepository productRepository, IMapper mapper)
        {
            _productRepository = productRepository;
            _mapper = mapper;
        }

        public async Task<BaseCommandResponse> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
        {
            var response = new BaseCommandResponse();
            var validator = new ProductUpdateValidator(_productRepository);
            var validationResult = await validator.ValidateAsync(request.ProductUpdateDto);

            if (!validationResult.IsValid)
            {
                response.Success = false;
                response.Message = "Product updation failed.";
                response.Errors = validationResult.Errors.Select(q => q.ErrorMessage).Distinct().ToList();

                return response;
            }
            else
            {
                response.Success = true;
                response.Message = "Product creation successful.";
            }

            var product = await _productRepository.GetAsync(request.ProductUpdateDto.Id);
            _mapper.Map(request.ProductUpdateDto, product); // Map new product (request.Product) -> old product (product)

            await _productRepository.UpdateAsync(product);

            return response;
        }
    }
}
