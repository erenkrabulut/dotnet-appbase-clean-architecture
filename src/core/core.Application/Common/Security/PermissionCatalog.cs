using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace core.Application.Common.Security
{
    public static class PermissionCatalog
    {
        public static IReadOnlyCollection<string> GetAll()
        {
            Assembly assembly = typeof(PermissionCatalog).Assembly;

            var values = assembly
                .GetTypes()
                .Where(t =>
                    t is { IsClass: true, IsAbstract: true, IsSealed: true } && 
                    t.Name.EndsWith("Permissions", StringComparison.Ordinal))
                .SelectMany(t =>
                    t.GetFields(BindingFlags.Public | BindingFlags.Static | BindingFlags.FlattenHierarchy)
                     .Where(f => f.IsLiteral && !f.IsInitOnly && f.FieldType == typeof(string))
                     .Select(f => (string)f.GetRawConstantValue()!))
                .Where(v => !string.IsNullOrWhiteSpace(v))
                .Distinct(StringComparer.Ordinal)
                .OrderBy(v => v, StringComparer.Ordinal)
                .ToArray();

            return values;
        }
    }
}
