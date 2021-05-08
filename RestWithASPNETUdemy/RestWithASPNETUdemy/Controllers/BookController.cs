using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

using RestWithASPNETUdemy.Business;
using RestWithASPNETUdemy.Hypermedia.Filters;
using RestWithASPNETUdemy.Model;

namespace RestWithASPNETUdemy.Controllers
{
    [ApiVersion("1")]
    [ApiController]
    [Route("api/[controller]/v{version:apiVersion}")]
    public class BookController : ControllerBase
    {
        private readonly ILogger<BookController> _logger;
        private IBookBusiness _bookBusiness;

        public BookController(ILogger<BookController> logger, IBookBusiness bookBusiness)
        {
            _logger = logger;
            _bookBusiness = bookBusiness;
        }

        [HttpGet]
        [TypeFilter(typeof(HyperMediaFilter))] // Anotação para o HATEOAS
        public IActionResult Get()
        {
            return Ok(_bookBusiness.FindAll());
        }

        [HttpGet("{id}")]
        [TypeFilter(typeof(HyperMediaFilter))] // Anotação para o HATEOAS
        public IActionResult Get(long id)
        {
            var book = _bookBusiness.FindById(id);
            if (book == null) return NotFound();
            return Ok(book);
        }

        [HttpPost("{id}")]
        [TypeFilter(typeof(HyperMediaFilter))] // Anotação para o HATEOAS
        public IActionResult Post([FromBody] BookVO book)
        {
            
            if (book == null) return BadRequest();
            return Ok(_bookBusiness.Create(book));
        }

        [HttpPut("{id}")]
        [TypeFilter(typeof(HyperMediaFilter))] // Anotação para o HATEOAS
        public IActionResult Put([FromBody] BookVO book)
        {

            if (book == null) return BadRequest();
            return Ok(_bookBusiness.Update(book));
        }

        [HttpDelete("{id}")]
        
        public IActionResult Delete(long id)
        {
            _bookBusiness.Delete(id);
            return NoContent();
        }
    }
}
