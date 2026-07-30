using FluentValidation;

namespace core.Application.Features.Auth.Commands.GoogleLogin
{
    public sealed class GoogleLoginCommandValidator : AbstractValidator<GoogleLoginCommand>
    {
        public GoogleLoginCommandValidator()
        {
            RuleFor(x => x.IdToken)
                .NotEmpty();
        }
    }
}
