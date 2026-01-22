using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace core.Application.Common.Responses
{
    public sealed class ExceptionResponse
    {
        public string Title { get; init; } = default!;
        public string Detail { get; init; } = default!;
        public int Status { get; init; }    
        public string Type { get; init; } = default!;
        public string Code { get; init; } = default!;
    }
}
