using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace core.Application.Features.Users.Queries.GetUserByEmail
{
    public sealed class GetUserByEmailQueryValidator : AbstractValidator<GetUserByEmailQuery>
    {
        public GetUserByEmailQueryValidator()
        {
            RuleFor(x => x.Email)
                .NotEmpty()
                .EmailAddress();
        }
    }
}
