using core.Domain.Common;



namespace core.Domain.Entities.Identity
{
    public class User: Entity<Guid> 
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public bool IsActive { get; set; }


        public virtual ICollection<UserLogin> Logins { get; set; } = null!;
        public virtual ICollection<RefreshToken> RefreshTokens { get; set; } = null!;
        public virtual ICollection<UserRole> UserRoles { get; set; } = null!;


        public User(string firstName, string lastName, string email)
        {
            Id = Guid.NewGuid();
            FirstName = firstName;
            LastName = lastName;
            Email = email;
        }

    }
}
