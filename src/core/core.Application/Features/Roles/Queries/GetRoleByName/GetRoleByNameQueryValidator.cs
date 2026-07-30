using core.Application.Features.Roles.Constants;
using FluentValidation;

namespace core.Application.Features.Roles.Queries.GetRoleByName
{
    public sealed class GetRoleByNameQueryValidator : AbstractValidator<GetRoleByNameQuery>
    {
        public GetRoleByNameQueryValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty()
                .MaximumLength(RolesConstants.NameMaxLength);
        }
    }
}
