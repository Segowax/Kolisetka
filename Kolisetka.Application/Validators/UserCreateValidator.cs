using FluentValidation;
using Kolisetka.Application.Contracts.Identity;
using Kolisetka.Application.Features.User.Requests.Commands;
using Kolisetka.Application.Properties;

namespace Kolisetka.Application.Validators
{
    public class UserCreateValidator : AbstractValidator<CreateUserCommand>
    {
        private readonly IAuthRepository _authRepository;

        public UserCreateValidator(IAuthRepository authRepository)
        {
            _authRepository = authRepository;

            RuleFor(prop => prop.User.Email)
                .NotNull().WithMessage(Resources.User_Validator_Required)
                .NotEmpty().WithMessage(Resources.User_Validator_Required)
                .EmailAddress().WithMessage(Resources.User_Validator_Email)
                .MaximumLength(256).WithMessage(Resources.User_Validator_TooLong)
                .MustAsync(async (email, token) =>
                 {
                     return !await _authRepository.IsEmailExist(email);
                 }).WithMessage(Resources.User_Validator_Exist);
            RuleFor(prop => prop.User.FirstName)
                .NotNull().WithMessage(Resources.User_Validator_Required)
                .NotEmpty().WithMessage(Resources.User_Validator_Required)
                .MaximumLength(256).WithMessage(Resources.User_Validator_TooLong);
            RuleFor(prop => prop.User.LastName)
                .NotNull().WithMessage(Resources.User_Validator_Required)
                .NotEmpty().WithMessage(Resources.User_Validator_Required)
                .MaximumLength(256).WithMessage(Resources.User_Validator_TooLong);
            RuleFor(prop => prop.Password)
                .NotNull().WithMessage(Resources.User_Validator_Required)
                .NotEmpty().WithMessage(Resources.User_Validator_Required)
                .MinimumLength(8).WithMessage(Resources.User_Validator_TooShort)
                .MaximumLength(256).WithMessage(Resources.User_Validator_TooLong)
                .Matches(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@$!%*?&])[A-Za-z\d@$!%*?&]{8,}$").WithMessage(Resources.User_Validator_Password);
            RuleFor(prop => prop.User.UserName)
                .NotNull().WithMessage(Resources.User_Validator_Required)
                .NotEmpty().WithMessage(Resources.User_Validator_Required)
                .MaximumLength(256).WithMessage(Resources.User_Validator_TooLong)
                .MustAsync(async (userName, token) =>
                {
                    return !await _authRepository.IsUserNameExist(userName);
                }).WithMessage(Resources.User_Validator_Exist);
        }
    }
}
