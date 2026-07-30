using FluentValidation;

namespace core.Application.Features.Permissions.Queries.GetPermissionById
{
    public sealed class GetPermissionByIdQueryValidator : AbstractValidator<GetPermissionByIdQuery>
    {
        public GetPermissionByIdQueryValidator()
        {
            RuleFor(x => x.Id).GreaterThan(0);
        }
    }
}
