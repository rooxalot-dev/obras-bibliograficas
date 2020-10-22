using Data.Context;
using Domain.Entites;
using Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Data.Repositories
{
    public class NamesRepository : INamesRepository, IDisposable
    {
        private ObrasContext _obrasContext;
        private List<Author> savedNames = new List<Author>();

        public NamesRepository(ObrasContext obrasContext)
        {
            _obrasContext = obrasContext;
        }

        public List<Author> GetAuthors(string name)
        {
            IEnumerable<Author> foundAuthors;

            if (!string.IsNullOrEmpty(name))
            {
                foundAuthors = _obrasContext.Author.Where((author) => author.FormattedName.ToLower().Contains(name));
            }
            else 
            {
                foundAuthors = _obrasContext.Author;
            }

            return foundAuthors.ToList();
        }

        public List<Author> SaveAuthors(List<Author> authors)
        {
            var namesList = authors.Select((author) => author.FormattedName);

            using (_obrasContext) 
            {
                _obrasContext.Author.AddRange(authors);
                _obrasContext.SaveChanges();

                var savedAuthors = _obrasContext.Author.Where((author) => namesList.Contains(author.FormattedName));

                return savedAuthors.ToList();
            }
        }

        public void Dispose()
        {
            _obrasContext.Dispose();
        }
    }
}
