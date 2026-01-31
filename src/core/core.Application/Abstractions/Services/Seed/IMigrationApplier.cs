using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace core.Application.Abstractions.Services.Seed
{
    public interface IMigrationApplier
    {
        Task ApplyAsync(CancellationToken ct = default);
    }
}
