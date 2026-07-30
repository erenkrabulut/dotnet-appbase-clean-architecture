using core.Application.Abstractions.Services.Identity;
using core.Application.Features.Roles.Constants;
using FluentValidation;

namespace core.Application.Features.Roles.Commands.CreateRole
{
    public sealed class CreateRoleCommandValidator : AbstractValidator<CreateRoleCommand>
    {

        public CreateRoleCommandValidator(IRoleService roleService)
        {

            RuleFor(x => x.Name)
                .NotEmpty()
                .MaximumLength(RolesConstants.NameMaxLength);

            RuleFor(x => x.Name)
                .MustAsync(async (name, ct) =>
                {
                    var existing = await roleService.TryGetByNameAsync(name, ct);
                    return existing is null;
                })
                .WithMessage("Role name already exists.");
        }

    }
}
