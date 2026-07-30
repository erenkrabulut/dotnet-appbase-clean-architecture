using FluentValidation;

namespace core.Application.Features.Roles.Queries.GetRoleById
{
    public sealed class GetRoleByIdQueryValidator : AbstractValidator<GetRoleByIdQuery>
    {
        public GetRoleByIdQueryValidator()
        {
            RuleFor(x => x.Id).NotEmpty();
        }
    }
}
