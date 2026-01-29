using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace core.Application.Features.Roles.Queries.GetRolesPage
{
    public sealed class GetRolesPageQueryValidator : AbstractValidator<GetRolesPageQuery>
    {
        public GetRolesPageQueryValidator()
        {
            RuleFor(x => x.PageRequest).NotNull();
            RuleFor(x => x.PageRequest.PageIndex).GreaterThan(0).LessThanOrEqualTo(200);
            RuleFor(x => x.PageRequest.PageSize).GreaterThan(0);
        }
    }
}
