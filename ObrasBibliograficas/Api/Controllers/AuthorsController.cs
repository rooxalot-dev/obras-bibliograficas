using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Entites;
using Domain.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthorsController : ControllerBase
    {
        private AuthorsService _namesService;

        public AuthorsController(AuthorsService namesService)
        {
            _namesService = namesService;
        }

        [HttpPost]
        public List<Author> Post([FromBody] List<Author> authors)
        {
            var savedAuthors = this._namesService.SaveAuthors(authors);

            return savedAuthors;
        }

        [HttpGet]
        public List<Author> Get(string name)
        {
            var savedAuthors = this._namesService.GetAuthors(name);

            return savedAuthors;
        }
    }
}
