using FluentValidation;

namespace core.Application.Features.Permissions.Queries.GetPermissionPage
{
    public sealed class GetPermissionsPageQueryValidator : AbstractValidator<GetPermissionsPageQuery>
    {
        public GetPermissionsPageQueryValidator()
        {
            RuleFor(x => x.PageRequest).NotNull();
            RuleFor(x => x.PageRequest.PageIndex).GreaterThanOrEqualTo(0);
            RuleFor(x => x.PageRequest.PageSize).GreaterThan(0).LessThanOrEqualTo(200);
        }
    }
}
