using core.Application.Abstractions.Services.Identity;
using core.Application.Features.Roles.Constants;
using core.Domain.Entities.Identity;
using FluentValidation;

namespace core.Application.Features.Roles.Commands.UpdateRole
{
    public sealed class UpdateRoleCommandValidator : AbstractValidator<UpdateRoleCommand>
    {
        public UpdateRoleCommandValidator(IRoleService roleService)
        {
            RuleFor(x => x.Id)
                .NotEmpty();

            RuleFor(x => x.Name)
                .NotEmpty()
                .MaximumLength(RolesConstants.NameMaxLength)
                .MustAsync(async (command, name, ct) =>
                {
                    Role? existing = await roleService.TryGetByIdAsync(command.Id, ct);

                    if (existing is null)
                    {
                        // if role is not exist handler will throw NotFound exception, so skip this here.
                        return true;
                    }

                    if (!string.Equals(existing.Name, name, System.StringComparison.OrdinalIgnoreCase))
                        return true;

                    var byName = await roleService.TryGetByNameAsync(name, ct);
                    return byName is null;

                })
                .WithMessage("Role name already exists.");
        }

    }
}
