using core.Application.Features.Roles.Constants;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace core.Application.Features.Roles.Queries.GetRoleByName
{
    public sealed class GetRoleByNameQueryValidator : AbstractValidator<GetRoleByNameQuery>
    {
        public GetRoleByNameQueryValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty()
                .MaximumLength(RolesConstants.NameMaxLength);
        }
    }
}
