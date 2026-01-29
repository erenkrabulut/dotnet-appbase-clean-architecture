using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
