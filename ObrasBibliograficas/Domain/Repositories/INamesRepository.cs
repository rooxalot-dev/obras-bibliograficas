using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Repositories
{
    public interface INamesRepository
    {
        List<string> SaveNames(List<string> names);
        List<string> GetNames(string name);
    }
}
