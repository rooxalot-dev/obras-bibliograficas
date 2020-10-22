using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Entites
{
    public class Author : BaseEntity
    {
        public string Name { get; set; }
        public string FormattedName { get; set; }
    }
}
