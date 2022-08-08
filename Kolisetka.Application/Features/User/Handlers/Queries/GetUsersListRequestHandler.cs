using Kolisetka.Application.DTOs.DtoUser;
using Kolisetka.Application.Features.User.Requests.Queries;
using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Kolisetka.Application.Features.User.Handlers.Queries
{
    public class GetUsersListRequestHandler : IRequestHandler<GetUsersListRequest, IReadOnlyList<UserGetDto>>
    {
        public Task<IReadOnlyList<UserGetDto>> Handle(GetUsersListRequest request, CancellationToken cancellationToken)
        {
            throw new System.NotImplementedException();
        }
    }
}
