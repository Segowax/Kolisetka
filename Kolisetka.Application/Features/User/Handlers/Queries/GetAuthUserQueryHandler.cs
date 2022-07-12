using Kolisetka.Application.Contracts.Persistence;
using Kolisetka.Application.Features.User.Requests.Queries;
using Kolisetka.Application.Responses;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Kolisetka.Application.Features.User.Handlers.Queries
{
    public class GetAuthUserQueryHandler : IRequestHandler<GetAuthUserQuery, AuthResponse>
    {
        private readonly IAuthRepository _authRepository;

        public GetAuthUserQueryHandler(IAuthRepository authRepository)
        {
            _authRepository = authRepository;
        }

        public Task<AuthResponse> Handle(GetAuthUserQuery request, CancellationToken cancellationToken)
        {
            return _authRepository.Login(request);
        }
    }
}
