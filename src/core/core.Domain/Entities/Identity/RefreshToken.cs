using core.Domain.Common;

namespace core.Domain.Entities.Identity
{
    public class RefreshToken : Entity<Guid>
    {
        public Guid UserId { get; set; }
        public string Token { get; set; }
        public DateTime Expires { get; set; }
        public DateTime Created { get; set; }
        public string CreatedByIp { get; set; }
        public DateTime? Revoked { get; set; }
        public string? RevokedByIp { get; set; }
        public string? ReplacedByToken { get; set; }
        public string? ReasonRevoked { get; set; }

        public bool IsExpired => DateTime.UtcNow >= Expires;
        public bool IsRevoked => Revoked != null;
        public bool IsActive => !IsRevoked && !IsExpired;

        public virtual User User { get; set; } = null!;


        public RefreshToken(Guid userId, string token, DateTime expires, string createdByIp)
        {
            Id = Guid.NewGuid();
            UserId = userId;
            Token = token;
            Expires = expires;
            CreatedByIp = createdByIp;
            Created = DateTime.UtcNow;
        }

        public void Revoke(string ipAddress, string reason, string? replacedByToken = null)
        {
            Revoked = DateTime.UtcNow;
            RevokedByIp = ipAddress;
            ReasonRevoked = reason;
            ReplacedByToken = replacedByToken;
        }
    }
}
