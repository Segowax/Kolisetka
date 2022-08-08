using Kolisetka.Application.DTOs.DtoUser;
using MediatR;
using System.Collections.Generic;

namespace Kolisetka.Application.Features.User.Requests.Queries
{
    public class GetUsersListRequest : IRequest<IReadOnlyList<UserGetDto>> { }
}
