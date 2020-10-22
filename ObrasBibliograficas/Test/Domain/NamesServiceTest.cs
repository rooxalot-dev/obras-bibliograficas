using Domain.Entites;
using Domain.Exceptions;
using Domain.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using Test.Mocks;
using Xunit;

namespace Test.Domain
{
    public class NamesServiceTest
    {
        private NamesRepositoryMock _repositoryMock;
        private NamesService _namesService;

        public NamesServiceTest() 
        {
            _repositoryMock = new NamesRepositoryMock();
            _namesService = new NamesService(_repositoryMock);
        }

        [Fact(DisplayName = "Deve lançar uma exceção caso a listagem esteja vazia ou nula")]
        public void DeveLancarExcecaoCasoListagemEstejaVaziaOuNula()
        {
            Assert.Throws<DomainException>(() => {
                _namesService.SaveAuthors(null);
            });

            Assert.Throws<DomainException>(() => {
                _namesService.SaveAuthors(new List<Author>());
            });
        }

        [Fact(DisplayName = "Deve retornar o ultimo sobrenome do nome completo informado totalmente em maiusculo")]
        public void DeveRetornarUltimoSobrenomeMaiusculo()
        {
            var authorsList = new List<Author> { new Author { Name = "rodrigo martins de azevedo" } };

            var savedAuthors = _namesService.SaveAuthors(authorsList);
            var savedAuthor = savedAuthors.FirstOrDefault();

            Assert.Equal("AZEVEDO, Rodrigo Martins de", savedAuthor.FormattedName);
        }

        [Fact(DisplayName = "Deve retornar o único nome informado totalmente em maíusculo")]
        public void DeveRetornarUnicoNomeTotalmenteMaiusculo()
        {
            var authorsList = new List<Author> { new Author { Name = "Guimarães" } };

            var savedAuthors = _namesService.SaveAuthors(authorsList);
            var savedAuthor = savedAuthors.FirstOrDefault();

            Assert.Equal("GUIMARÃES", savedAuthor.FormattedName);
        }

        [Fact(DisplayName = "Adiciona outras partes do nome em maíusculo caso o ultimo sobrenome seja familiar (?)")]
        public void AdicionarPartesNomeMaiusculoCasoUltimoSobrenomeSejaFamiliar()
        {
            var authorsList = new List<Author> { 
                new Author { Name = "Joao Silva Neto" },
                new Author { Name = "Joao Neto" },
            };

            var savedAuthors = _namesService.SaveAuthors(authorsList);
            var firstSavedName = savedAuthors.FirstOrDefault()?.FormattedName;
            var lastSavedName = savedAuthors.LastOrDefault()?.FormattedName;

            Assert.Equal("SILVA NETO, Joao", firstSavedName);
            Assert.Equal("NETO, Joao", lastSavedName);
        }
    }
}
