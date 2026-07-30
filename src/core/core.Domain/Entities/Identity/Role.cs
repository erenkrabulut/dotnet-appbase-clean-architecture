using core.Domain.Common;

namespace core.Domain.Entities.Identity
{
    public class Role : Entity<Guid>
    {
        public string Name { get; set; }

        public virtual ICollection<UserRole> UserRoles { get; set; } = new List<UserRole>();
        public virtual ICollection<RolePermission> RolePermissions { get; set; } = new List<RolePermission>();

        public Role(string name)
        {
            Name = name;
        }
    }
}
