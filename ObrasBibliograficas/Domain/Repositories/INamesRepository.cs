using Domain.Entites;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Repositories
{
    public interface INamesRepository
    {
        List<Author> SaveAuthors(List<Author> authors);
        List<Author> GetAuthors(string name);
    }
}
