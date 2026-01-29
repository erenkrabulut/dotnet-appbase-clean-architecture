using core.Application.Abstractions.Services.Identity;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace core.Application.Features.UserLogins.Queries.GetUserLoginsByUserId
{
    public sealed class GetUserLoginsByUserIdQueryValidator : AbstractValidator<GetUserLoginsByUserIdQuery>
    {
        private readonly IUserService _userService;

        public GetUserLoginsByUserIdQueryValidator(IUserService userService)
        {
            _userService = userService;

            RuleFor(x => x.UserId).NotEmpty();

            RuleFor(x => x)
                .MustAsync(UserExistsAsync)
                .WithMessage("User does not exist.");
        }

        private async Task<bool> UserExistsAsync(GetUserLoginsByUserIdQuery query, CancellationToken ct)
        {
            var user = await _userService.TryGetByIdAsync(query.UserId, ct);
            return user is not null;
        }
    }
}
