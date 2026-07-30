using FluentValidation;

namespace core.Application.Features.Roles.Queries.GetRolesPage
{
    public sealed class GetRolesPageQueryValidator : AbstractValidator<GetRolesPageQuery>
    {
        public GetRolesPageQueryValidator()
        {
            RuleFor(x => x.PageRequest).NotNull();
            RuleFor(x => x.PageRequest.PageIndex).GreaterThanOrEqualTo(0);
            RuleFor(x => x.PageRequest.PageSize).GreaterThan(0).LessThanOrEqualTo(200);
        }
    }
}
