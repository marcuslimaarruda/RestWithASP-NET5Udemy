using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using RestWithASPNETUdemy.Business;
using RestWithASPNETUdemy.Data.VO;
using RestWithASPNETUdemy.Hypermedia.Filters;
using System.Collections.Generic;

namespace RestWithASPNETUdemy.Controllers
{
    [ApiVersion("1")]
    [ApiController]
    [Route("api/[controller]/v{version:apiVersion}")]
    public class PersonController : ControllerBase
    {
        private readonly ILogger<PersonController> _logger;
        private IPersonBusiness _personBusiness;

        public PersonController(ILogger<PersonController> logger, IPersonBusiness personBusiness)
        {
            _logger = logger;
            _personBusiness = personBusiness;
        }

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(List<PersonVO>))] // Customização do swagger
        [ProducesResponseType(204)] // Customização do swagger
        [ProducesResponseType(400)] // Customização do swagger
        [ProducesResponseType(401)] // Customização do swagger
        [TypeFilter(typeof(HyperMediaFilter))] // Anotação para o HATEOAS
        public IActionResult Get()
        {
            return Ok(_personBusiness.FindAll());
        }

        [HttpGet("{id}")]
        [ProducesResponseType(200, Type = typeof(PersonVO))] // Customização do swagger
        [ProducesResponseType(204)] // Customização do swagger
        [ProducesResponseType(400)] // Customização do swagger
        [ProducesResponseType(401)] // Customização do swagger
        [TypeFilter(typeof(HyperMediaFilter))] // Anotação para o HATEOAS
        public IActionResult Get(long id)
        {
            var person = _personBusiness.FindById(id);
            if (person == null) return NotFound();
            return Ok(person);
        }

        [HttpPost("{id}")]
        [ProducesResponseType(200, Type = typeof(PersonVO))] // Customização do swagger
        [ProducesResponseType(400)] // Customização do swagger
        [ProducesResponseType(401)] // Customização do swagger
        [TypeFilter(typeof(HyperMediaFilter))] // Anotação para o HATEOAS
        public IActionResult Post([FromBody] PersonVO person)
        {
            
            if (person == null) return BadRequest();
            return Ok(_personBusiness.Create(person));
        }

        [HttpPut("{id}")]
        [ProducesResponseType(200, Type = typeof(PersonVO))] // Customização do swagger
        [ProducesResponseType(400)] // Customização do swagger
        [ProducesResponseType(401)] // Customização do swagger
        [TypeFilter(typeof(HyperMediaFilter))] // Anotação para o HATEOAS
        public IActionResult Put([FromBody] PersonVO person)
        {

            if (person == null) return BadRequest();
            return Ok(_personBusiness.Update(person));
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(204)] // Customização do swagger
        [ProducesResponseType(400)] // Customização do swagger
        [ProducesResponseType(401)] // Customização do swagger
        public IActionResult Delete(long id)
        {
            _personBusiness.Delete(id);
            return NoContent();
        }
    }
}
