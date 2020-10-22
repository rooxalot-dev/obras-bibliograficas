using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Entites
{
    public class BaseEntity
    {
        public Guid ID { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime UpdatedAt { get; set; } = DateTime.Now;
    }
}
