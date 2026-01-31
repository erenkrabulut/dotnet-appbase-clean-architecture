using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace core.Application.Abstractions.Services.Seed
{
    public interface ISeeder
    {
        int Order { get; }
        Task SeedAsync(CancellationToken ct = default);
    }
}
