using AutoMapper;
using Kolisetka.Application.Contracts.Identity;
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
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;

        public GetUsersListRequestHandler(IUserRepository userRepository, IMapper mapper)
        {
            _userRepository = userRepository;
            _mapper = mapper;
        }

        public async Task<IReadOnlyList<UserGetDto>> Handle(GetUsersListRequest request, CancellationToken cancellationToken)
        {
            var users = await _userRepository.GetAllUsersAsync();

            return _mapper.Map<IReadOnlyList<UserGetDto>>(users);
        }
    }
}
