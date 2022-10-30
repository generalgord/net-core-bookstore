using Microsoft.AspNetCore.Mvc;
using WebApi.Entities;
using WebApi.Repositories;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AuthorsController : ControllerBase
    {
        readonly IAuthorRepository _authorRepository;

        public AuthorsController(IAuthorRepository authorRepository)
        {
            _authorRepository = authorRepository;
        }

        [HttpGet]
        public ActionResult<List<Author>> GetAll()
        {
            return Ok(_authorRepository.GetAuthors());
        }

        [HttpPost]
        public IActionResult AddBook([FromBody] Author newBook)
        {
            return Ok(_authorRepository.AddAuthor(newBook));
        }

        [HttpPut("{id}")]
        public IActionResult UpdateBook(int id, [FromBody] Author updatedAuthor)
        {
            return Ok(_authorRepository.UpdateAuthor(id, updatedAuthor));
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteBook(int id)
        {
            return Ok(_authorRepository.RemoveAuthor(id));
        }
    }
}
