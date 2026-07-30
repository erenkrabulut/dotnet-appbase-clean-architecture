using core.Application.Features.Permissions.Constants;
using FluentValidation;

namespace core.Application.Features.Permissions.Queries.GetPermissionByName
{
    public sealed class GetPermissionByNameQueryValidator : AbstractValidator<GetPermissionByNameQuery>
    {
        public GetPermissionByNameQueryValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty()
                .MaximumLength(PermissionsConstants.NameMaxLength);
        }
    }
}
