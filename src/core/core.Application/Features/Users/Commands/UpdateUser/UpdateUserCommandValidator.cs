using core.Application.Abstractions.Services.Identity;
using core.Domain.Entities.Identity;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace core.Application.Features.Users.Commands.UpdateUser
{
    public sealed class UpdateUserCommandValidator : AbstractValidator<UpdateUserCommand>
    {
        private const int NameMaxLength = 100;

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

                    if (!string.Equals(existing.Email, email, System.StringComparison.OrdinalIgnoreCase))
                    {
                        await userService.EnsureEmailUniqueAsync(email, ct);
                    }

                    return true;
                });

            RuleFor(x => x.FirstName)
                .NotEmpty()
                .MaximumLength(NameMaxLength);

            RuleFor(x => x.LastName)
                .NotEmpty()
                .MaximumLength(NameMaxLength);
        }
    }
}
