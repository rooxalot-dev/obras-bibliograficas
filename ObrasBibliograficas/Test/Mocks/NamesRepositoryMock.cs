using Domain.Entites;
using Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace Test.Mocks
{
    public class NamesRepositoryMock : INamesRepository
    {
        private List<Author> savedAuthors = new List<Author>();

        public List<Author> GetAuthors(string name)
        {
            return savedAuthors;
        }

        public List<Author> SaveAuthors(List<Author> authors)
        {
            savedAuthors.AddRange(authors);
            return authors;
        }
    }
}
