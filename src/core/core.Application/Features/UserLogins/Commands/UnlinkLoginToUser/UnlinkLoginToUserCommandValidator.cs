using core.Application.Abstractions.Services.Identity;
using FluentValidation;

namespace core.Application.Features.UserLogins.Commands.UnlinkLoginToUser
{
    public sealed class UnlinkLoginCommandValidator : AbstractValidator<UnlinkLoginCommand>
    {
        private readonly IUserService _userService;

        public UnlinkLoginCommandValidator(IUserService userService)
        {
            _userService = userService;

            RuleFor(x => x.UserId).NotEmpty();
            RuleFor(x => x.Provider).IsInEnum();

            RuleFor(x => x)
                .MustAsync(UserExistsAsync)
                .WithMessage("User does not exist.");
        }

        private async Task<bool> UserExistsAsync(UnlinkLoginCommand cmd, CancellationToken ct)
        {
            var user = await _userService.TryGetByIdAsync(cmd.UserId, ct);
            return user is not null;
        }
    }
}
