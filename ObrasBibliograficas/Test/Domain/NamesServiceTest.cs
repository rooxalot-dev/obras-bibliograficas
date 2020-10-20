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
                _namesService.SaveNames(null);
            });

            Assert.Throws<DomainException>(() => {
                _namesService.SaveNames(new List<string>());
            });
        }

        [Fact(DisplayName = "Deve retornar o ultimo sobrenome do nome completo informado totalmente em maiusculo")]
        public void DeveRetornarUltimoSobrenomeMaiusculo()
        {
            var namesList = new List<string> { "rodrigo martins de azevedo" };

            var savedNames = _namesService.SaveNames(namesList);
            var savedName = savedNames.FirstOrDefault();

            Assert.Equal("AZEVEDO, Rodrigo Martins de", savedName);
        }

        [Fact(DisplayName = "Deve retornar o único nome informado totalmente em maíusculo")]
        public void DeveRetornarUnicoNomeTotalmenteMaiusculo()
        {
            var namesList = new List<string> { "Guimarães" };

            var savedNames = _namesService.SaveNames(namesList);
            var savedName = savedNames.FirstOrDefault();

            Assert.Equal("GUIMARÃES", savedName);
        }

        [Fact(DisplayName = "Adiciona outras partes do nome em maíusculo caso o ultimo sobrenome seja familiar (?)")]
        public void AdicionarPartesNomeMaiusculoCasoUltimoSobrenomeSejaFamiliar()
        {
            var namesList = new List<string> { "Joao Silva Neto", "Joao Neto" };

            var savedNames = _namesService.SaveNames(namesList);
            var firstSavedName = savedNames.FirstOrDefault();
            var lastSavedName = savedNames.LastOrDefault();

            Assert.Equal("SILVA NETO, Joao", firstSavedName);
            Assert.Equal("NETO, Joao", lastSavedName);
        }
    }
}
