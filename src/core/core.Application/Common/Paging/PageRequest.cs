using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace core.Application.Common.Paging
{
    public sealed class PageRequest
    {
        public int PageIndex { get; init; } = 0;
        public int PageSize { get; init; } = 10;

        public string? OrderBy { get; init; }
        public bool Desc { get; init; } = false;
    }
}
