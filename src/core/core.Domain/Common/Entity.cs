using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace core.Domain.Common
{
    public class Entity<T> : IAuditable
    {
        public T Id { get; set; }
        public DateTime CreatedAt {get; set; }
        public DateTime? UpdatedAt { get; set; }
        public DateTime? DeletedAt { get; set; }
    }
}
