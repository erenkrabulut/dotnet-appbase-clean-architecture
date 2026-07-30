using core.Application.Abstractions.Services.Identity;
using core.Domain.Entities.Identity;
using FluentValidation;

namespace core.Application.Features.UserRoles.Queries.GetUserRolesByUserId
{
    public sealed class GetUserRolesByUserIdQueryValidator : AbstractValidator<GetUserRolesByUserIdQuery>
    {
        public GetUserRolesByUserIdQueryValidator(IUserService userService)
        {

            RuleFor(x => x.UserId).NotEmpty();

            RuleFor(x => x)
                .MustAsync(async (command, userId, ct) =>
                {
                    User? existing = await userService.TryGetByIdAsync(command.UserId, ct);
                    return existing is not null;
                })
                .WithMessage("User does not exist."); ;
        }

    }
}
