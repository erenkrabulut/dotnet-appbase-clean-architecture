using core.Application.Abstractions.Services.Identity;
using core.Domain.Entities.Identity;
using FluentValidation;

namespace core.Application.Features.Users.Commands.UpdateUser
{
    public sealed class UpdateUserCommandValidator : AbstractValidator<UpdateUserCommand>
    {
        private const int _nameMaxLength = 100;

        public UpdateUserCommandValidator(IUserService userService)
        {
            RuleFor(x => x.Id)
                .NotEmpty();

            RuleFor(x => x.Email)
                .NotEmpty()
                .EmailAddress()
                .MustAsync(async (command, email, ct) =>
                {
                    User? existing = await userService.TryGetByIdAsync(command.Id, ct);

                    if (existing is null)
                    {
                        // if user is not exist handler will throw NotFound exception, so skip this here.
                        return true;
                    }

                    if (string.Equals(existing.Email, email, StringComparison.OrdinalIgnoreCase))
                        return true;



                    User? byEmail = await userService.TryGetByEmailAsync(email, ct);
                    return byEmail is null;
                });

            RuleFor(x => x.FirstName)
                .NotEmpty()
                .MaximumLength(_nameMaxLength);

            RuleFor(x => x.LastName)
                .NotEmpty()
                .MaximumLength(_nameMaxLength);
        }
    }
}
