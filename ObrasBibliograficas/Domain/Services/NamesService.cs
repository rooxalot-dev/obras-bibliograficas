using Domain.Entites;
using Domain.Exceptions;
using Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;

namespace Domain.Services
{
    public class AuthorsService
    {
        private INamesRepository _namesRepository;

        public AuthorsService(INamesRepository namesRepository) 
        {
            _namesRepository = namesRepository;
        }

        public List<Author> SaveAuthors(List<Author> authors)
        {
            if (authors == null || authors.Count == 0)
            {
                throw new DomainException("A lista de nomes deve ser informada!");
            }

            var formattedAuthors = authors.Select((author) => {
                var formattedName = FormatName(author.Name);
                author.FormattedName = formattedName;

                return author;
            }).ToList();

            var savedAuthors = this._namesRepository.SaveAuthors(formattedAuthors);

            return savedAuthors;
        }

        public List<Author> GetAuthors(string nameFilter)
        {
            var savedAuthors = this._namesRepository.GetAuthors(nameFilter);

            return savedAuthors;
        }

        private string FormatName(string name) 
        {
            var formattedName = new StringBuilder("");

            TextInfo textInfo = new CultureInfo("pt-BR", false).TextInfo;

            var excludedTitleCase = new string[] { "da", "de", "do", "das", "dos" };
            var familiarNames = new string[] { "FILHO", "FILHA", "NETO", "NETA", "SOBRINHO", "SOBRINHA", "JUNIOR" };

            // Remove espaços no inicio e final da string
            name = name.Trim();

            // Divide o nome separando em espaços
            var nameSplit = name.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries).ToList();

            // Retorna o ultimo sobrenome e o remove da lista de trechos do nome
            var lastName = nameSplit.LastOrDefault();
            nameSplit.Remove(lastName);

            var hasCoupleLastParts = nameSplit.Count() >= 2;
            var isExcludedFamiliarName = familiarNames.Contains(lastName.ToUpper());

            // Trata casos onde o ultimo sobrenome deve ser composto com o penultimo sobrenome
            if (isExcludedFamiliarName && hasCoupleLastParts) 
            {
                var penultimateName = nameSplit.Where((namePart) => !namePart.Equals(lastName)).LastOrDefault();
                nameSplit.Remove(penultimateName);

                lastName = penultimateName + " " + lastName;
            }

            // Obtem os trechos do nome até que não seja o ultimo sobrenome
            var remainingName = nameSplit
                .TakeWhile((nameSplitValue) => !nameSplitValue.Equals(lastName))
                .Select((nameSplitValue) => {
                    var isExcludedNamePart = excludedTitleCase.Contains(nameSplitValue.ToLower());
                    var namePart = isExcludedNamePart ? nameSplitValue : textInfo.ToTitleCase(nameSplitValue);

                    return namePart;
                });

            // Concatena todos os demais trechos do nome em uma string única, separando em virgula caso exista registros
            var joinedName = string.Join(" ", remainingName.ToArray());
            joinedName = !string.IsNullOrEmpty(joinedName) ? ", " + joinedName : joinedName;

            formattedName.AppendFormat("{0}{1}", lastName.ToUpper(), joinedName);

            return formattedName.ToString();
        }
    }
}
