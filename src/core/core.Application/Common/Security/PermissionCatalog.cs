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

        private static readonly Lazy<string[]> _all =
            new Lazy<string[]>(LoadAll, isThreadSafe: true);

        public static string[] GetAll() => _all.Value;

        public static string[] GetAdmins()
            => GetAll()
                .Where(x => x.EndsWith(".admin", StringComparison.Ordinal))
                .ToArray();

        public static string[] GetReads()
            => GetAll()
                .Where(x => x.EndsWith(".read", StringComparison.Ordinal))
                .ToArray();

        public static string[] GetWrites()
            => GetAll()
                .Where(x => x.EndsWith(".write", StringComparison.Ordinal))
                .ToArray();

        public static string[] GetAdds()
            => GetAll()
                .Where(x => x.EndsWith(".add", StringComparison.Ordinal))
                .ToArray();

        public static string[] GetUpdates()
            => GetAll()
                .Where(x => x.EndsWith(".update", StringComparison.Ordinal))
                .ToArray();

        public static string[] GetDeletes()
            => GetAll()
                .Where(x => x.EndsWith(".delete", StringComparison.Ordinal))
                .ToArray();

        private static string[] LoadAll()
        {
            Assembly assembly = typeof(PermissionCatalog).Assembly;

            var values = assembly
                .GetTypes()
                .Where(t =>
                    t is { IsClass: true, IsAbstract: true } 
                    && t.Name.EndsWith("Permissions", StringComparison.Ordinal) 
                    && (t.Namespace?.Contains(".Features.", StringComparison.Ordinal) ?? false)
                )
                .SelectMany(t => t.GetFields(BindingFlags.Public | BindingFlags.Static | BindingFlags.FlattenHierarchy))
                .Where(f =>
                    f.FieldType == typeof(string)
                    && f.IsLiteral
                    && !f.IsInitOnly 
                )
                .Select(f => (string)f.GetRawConstantValue()!)
                .Where(s => !string.IsNullOrWhiteSpace(s))
                .Distinct(StringComparer.Ordinal)
                .OrderBy(s => s, StringComparer.Ordinal)
                .ToArray();

            return values;
        }
    }

}
