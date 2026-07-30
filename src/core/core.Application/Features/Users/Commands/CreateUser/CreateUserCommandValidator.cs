using core.Application.Abstractions.Services.Identity;
using core.Domain.Entities.Identity;
using FluentValidation;

namespace core.Application.Features.Users.Commands.CreateUser
{
    public sealed class CreateUserCommandValidator : AbstractValidator<CreateUserCommand>
    {
        private const int _nameMaxLength = 100;

        public CreateUserCommandValidator(IUserService userService)
        {
            RuleFor(x => x.Email)
                .NotEmpty()
                .EmailAddress()
                .MustAsync(async (email, ct) =>
                {
                    User? byEmail = await userService.TryGetByEmailAsync(email, ct);
                    return byEmail is null;
                })
                .WithMessage("Email already exists.");

            RuleFor(x => x.FirstName)
                .NotEmpty()
                .MaximumLength(_nameMaxLength);

            RuleFor(x => x.LastName)
                .NotEmpty()
                .MaximumLength(_nameMaxLength);
        }
    }
}
