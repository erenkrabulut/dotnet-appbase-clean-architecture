namespace core.Domain.Common
{
    public class Entity<T> : IAuditable, ISoftDelete
    {
        public T Id { get; set; } = default!;
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public DateTime? DeletedAt { get; set; }
    }
}
