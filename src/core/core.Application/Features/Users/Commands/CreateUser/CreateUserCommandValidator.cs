using core.Application.Abstractions.Services.Identity;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace core.Application.Features.Users.Commands.CreateUser
{
    public sealed class CreateUserCommandValidator : AbstractValidator<CreateUserCommand>
    {
        private const int NameMaxLength = 100;

        public CreateUserCommandValidator(IUserService userService)
        {
            RuleFor(x => x.Email)
                .NotEmpty()
                .EmailAddress()
                .MustAsync(async (email, ct) =>
                {
                    await userService.EnsureEmailUniqueAsync(email, ct);
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
