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
    public class DeleteProductCommandHandler : IRequestHandler<DeleteProductCommand, BaseCommandResponse>
    {
        private readonly IProductRepository _productRepository;
        private readonly IMapper _mapper;

        public DeleteProductCommandHandler(IProductRepository productRepository, IMapper mapper)
        {
            _productRepository = productRepository;
            _mapper = mapper;
        }

        public async Task<BaseCommandResponse> Handle(DeleteProductCommand request, CancellationToken cancellationToken)
        {
            var response = new BaseCommandResponse();
            var validator = new ProductDeleteValidator(_productRepository);
            var validationResult = await validator.ValidateAsync(request.ProductDeleteDto);

            if (!validationResult.IsValid)
            {
                response.Success = false;
                response.Message = "Product deletion failed.";
                response.Errors = validationResult.Errors.Select(q => q.ErrorMessage).Distinct().ToList();

                return response;
            }
            else
            {
                response.Success = true;
                response.Message = "Product deletion successful.";
            }

            var product = await _productRepository.GetAsync(request.ProductDeleteDto.Id);
            _mapper.Map(request.ProductDeleteDto, product);

            await _productRepository.DeleteAsync(product);

            return response;
        }
    }
}
