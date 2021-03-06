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
        private AuthorsService _namesService;

        public NamesServiceTest() 
        {
            _repositoryMock = new NamesRepositoryMock();
            _namesService = new AuthorsService(_repositoryMock);
        }

        [Fact(DisplayName = "Deve lan�ar uma exce��o caso a listagem esteja vazia ou nula")]
        public void DeveLancarExcecaoCasoListagemEstejaVaziaOuNula()
        {
            Assert.Throws<DomainException>(() => {
                _namesService.SaveAuthors(null);
            });

            Assert.Throws<DomainException>(() => {
                _namesService.SaveAuthors(new List<Author>());
            });
        }

        [Fact(DisplayName = "Deve lan�ar uma exce��o caso algum dos nomes esteja vazio ou nulo")]
        public void DeveLancarExcecaoCasoAlgumNomeEstejaVazioOuNulo()
        {
            var authorsList1 = new List<Author> { new Author { Name = "" } };
            var authorsList2 = new List<Author> { new Author { Name = null } };

            Assert.Throws<DomainException>(() => {
                _namesService.SaveAuthors(authorsList1);
            });

            Assert.Throws<DomainException>(() => {
                _namesService.SaveAuthors(authorsList2);
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

        [Fact(DisplayName = "Deve retornar o �nico nome informado totalmente em ma�usculo")]
        public void DeveRetornarUnicoNomeTotalmenteMaiusculo()
        {
            var authorsList = new List<Author> { new Author { Name = "Guimar�es" } };

            var savedAuthors = _namesService.SaveAuthors(authorsList);
            var savedAuthor = savedAuthors.FirstOrDefault();

            Assert.Equal("GUIMAR�ES", savedAuthor.FormattedName);
        }

        [Fact(DisplayName = "Adiciona outras partes do nome em ma�usculo caso o ultimo sobrenome seja familiar (?)")]
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
