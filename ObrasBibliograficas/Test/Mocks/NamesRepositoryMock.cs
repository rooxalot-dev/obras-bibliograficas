using Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace Test.Mocks
{
    public class NamesRepositoryMock : INamesRepository
    {
        private List<string> savedNames = new List<string>();

        public List<string> GetNames(string name)
        {
            return savedNames;
        }

        public List<string> SaveNames(List<string> names)
        {
            savedNames.AddRange(names);
            return names;
        }
    }
}
