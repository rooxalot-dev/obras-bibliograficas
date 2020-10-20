using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NamesController : ControllerBase
    {
        private NamesService _namesService;

        public NamesController(NamesService namesService)
        {
            _namesService = namesService;
        }

        [HttpPost]
        public List<string> Post([FromBody] List<string> namesList)
        {
            var formatedNames = this._namesService.SaveNames(namesList);

            return formatedNames;
        }

        [HttpGet]
        public List<string> Get(string nameFilter)
        {
            var savedNames = this._namesService.GetNames(nameFilter);

            return savedNames;
        }
    }
}
