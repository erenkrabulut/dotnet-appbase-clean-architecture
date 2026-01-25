using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace core.Application.Features.Auth.Commands.GoogleRegister
{
    public sealed class GoogleRegisterCommandValidator : AbstractValidator<GoogleRegisterCommand>
    {
        public GoogleRegisterCommandValidator()
        {
            RuleFor(x => x.IdToken)
                .NotEmpty();

            RuleFor(x => x.FirstName)
                .NotEmpty();

            RuleFor(x => x.LastName)
                .NotEmpty();
        }
    }
}
