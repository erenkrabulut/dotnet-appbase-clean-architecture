using core.Domain.Common;

namespace core.Domain.Entities.Identity
{
    public class Permission : Entity<int>
    {
        public string Name { get; set; }

        public virtual ICollection<RolePermission> RolePermissions { get; set; } = null!;


        public Permission(string name)
        {
            Name = name;
            RolePermissions = new List<RolePermission>();
        }

    }
}
