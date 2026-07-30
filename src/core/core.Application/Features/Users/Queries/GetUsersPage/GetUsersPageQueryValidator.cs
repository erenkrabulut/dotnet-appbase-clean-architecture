using FluentValidation;

namespace core.Application.Features.Users.Queries.GetUsersPage
{
    public sealed class GetUsersPageQueryValidator : AbstractValidator<GetUsersPageQuery>
    {
        public GetUsersPageQueryValidator()
        {
            RuleFor(x => x.PageRequest)
                .NotNull();

            RuleFor(x => x.PageRequest.PageIndex)
                .GreaterThanOrEqualTo(0);

            RuleFor(x => x.PageRequest.PageSize)
                .GreaterThan(0)
                .LessThanOrEqualTo(200);
        }
    }
}
