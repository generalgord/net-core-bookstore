using Microsoft.AspNetCore.Mvc;
using WebApi.Commands.BookOperations;
using WebApi.DBOperations;
using WebApi.Repositories;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class BooksController : ControllerBase
    {
        readonly IBookRepository _bookRepository;
        readonly BookStoreDbContext _context;

        public BooksController(IBookRepository bookRepository, BookStoreDbContext context)
        {
            _bookRepository = bookRepository;
            _context = context;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            try
            {
                QueryGetBooks query = new QueryGetBooks(_context);
                return Ok(query.Handle());
            }
            catch (System.Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        public IActionResult AddBook([FromBody] CreateBookModel newBook)
        {
            try
            {
                CreateBookCommand command = new CreateBookCommand(_context, newBook);
                command.Handle();
                return Ok();
            }
            catch (System.Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{id}")]
        public IActionResult UpdateBook(int id, [FromBody] UpdateBookModel updatedBook)
        {
            try
            {
                UpdateBookCommand command = new UpdateBookCommand(_context, id, updatedBook);
                command.Handle();
                return Ok();
            }
            catch (System.Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("{id}")]
        public IActionResult GetBookById(int id)
        {
            try
            {
                QueryGetBookById query = new QueryGetBookById(_context, id);
                return Ok(query.Handle());
            }
            catch (System.Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteBook(int id)
        {
            try
            {
                DeleteBookCommand command = new DeleteBookCommand(_context, id);
                command.Handle();
                return Ok();
            }
            catch (System.Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
