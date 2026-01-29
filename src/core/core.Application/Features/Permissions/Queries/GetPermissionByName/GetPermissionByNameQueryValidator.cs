using core.Application.Features.Permissions.Constants;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace core.Application.Features.Permissions.Queries.GetPermissionByName
{
    public sealed class GetPermissionByNameQueryValidator : AbstractValidator<GetPermissionByNameQuery>
    {
        public GetPermissionByNameQueryValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty()
                .MaximumLength(PermissionsConstants.NameMaxLength);
        }
    }
}
