using FluentValidation;
using Kolisetka.Application.Contracts.Identity;
using Kolisetka.Application.Features.User.Requests.Queries;
using Kolisetka.Application.Properties;

namespace Kolisetka.Application.Validators
{
    public class UserGetValidator: AbstractValidator<GetUserRequest>
    {
        private readonly IAuthRepository _authRepository;

        public UserGetValidator(IAuthRepository authRepository)
        {
            _authRepository = authRepository;

            RuleFor(prop => prop.Email)
                .NotNull().WithMessage(Resources.User_Validator_NotExistsOrInvalidPassword)
                .NotEmpty().WithMessage(Resources.User_Validator_NotExistsOrInvalidPassword)
                .EmailAddress().WithMessage(Resources.User_Validator_NotExistsOrInvalidPassword)
                .MaximumLength(256).WithMessage(Resources.User_Validator_NotExistsOrInvalidPassword)
                .MustAsync(async (email, token) =>
                {
                    return await _authRepository.IsEmailExist(email);
                }).WithMessage(Resources.User_Validator_NotExistsOrInvalidPassword);
            RuleFor(prop => prop.Password)
                .NotNull().WithMessage(Resources.User_Validator_NotExistsOrInvalidPassword)
                .NotEmpty().WithMessage(Resources.User_Validator_NotExistsOrInvalidPassword)
                .MaximumLength(256).WithMessage(Resources.User_Validator_NotExistsOrInvalidPassword);
        }
    }
}
