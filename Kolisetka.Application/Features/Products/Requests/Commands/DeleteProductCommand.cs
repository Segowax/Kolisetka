using Kolisetka.Application.DTOs;
using MediatR;

namespace Kolisetka.Application.Features.Products.Requests.Commands
{
    public class DeleteProductCommand : IRequest
    {
        public int Id { get; set; }
    }
}
