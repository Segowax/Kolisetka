using AutoMapper;
using Kolisetka.Application.DTOs.DtoProduct;
using Kolisetka.Application.Validators;
using Kolisetka.Application.Features.Products.Requests.Commands;
using Kolisetka.Application.Contracts.Persistence;
using Kolisetka.Domain;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using Kolisetka.Application.Responses;
using System.Linq;
using Kolisetka.Application.Properties;

namespace Kolisetka.Application.Features.Products.Handlers.Commands
{
    public class CreateProductCommandHandler : IRequestHandler<CreateProductCommand, BaseCommandResponse>
    {
        private readonly IProductRepository _productRepository;
        private readonly IMapper _mapper;

        public CreateProductCommandHandler(IProductRepository productRepository, IMapper mapper)
        {
            _productRepository = productRepository;
            _mapper = mapper;
        }

        public async Task<BaseCommandResponse> Handle(CreateProductCommand request, CancellationToken cancellationToken)
        {
            var response = new BaseCommandResponse();
            var validator = new ProductCreateValidator();
            var validationResult = await validator.ValidateAsync(request.ProductCreateDto);

            if (!validationResult.IsValid)
            {
                response.Success = false;
                response.Message = Resources.Product_Creation_Failure;
                response.Errors = validationResult.Errors.Select(q => q.ErrorMessage).Distinct().ToList();

                return response;
            }
            else
            {
                response.Success = true;
                response.Message = Resources.Product_Creation_Success;
            }

            var product = _mapper.Map<ProductCreateDto, Product>(request.ProductCreateDto);
            await _productRepository.AddAsync(product);

            return response;
        }
    }
}
