using AutoMapper;
using FluentValidation;
using FluentValidation.Results;
using Microsoft.AspNetCore.Mvc;
using WebApi.DBOperations;
using WebApi.Operations.BookOperations.Commands;
using WebApi.Operations.BookOperations.Queries;
using WebApi.Repositories;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class BooksController : ControllerBase
    {
        readonly IBookRepository _bookRepository;
        readonly BookStoreDbContext _context;
        readonly IMapper _mapper;

        public BooksController(
            IBookRepository bookRepository,
            BookStoreDbContext context,
            IMapper mapper
        )
        {
            _bookRepository = bookRepository;
            _context = context;
            _mapper = mapper;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            try
            {
                QueryGetBooks query = new QueryGetBooks(_context, _mapper);
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
                CreateBookCommand command = new CreateBookCommand(_context, _mapper, newBook);

                var validator = new CreateBookCommandValidator();
                validator.ValidateAndThrow(command);
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
                var validator = new UpdateBookCommandValidator();
                validator.ValidateAndThrow(command);
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
                QueryGetBookById query = new QueryGetBookById(_context, _mapper, id);

                var validator = new QueryGetBookByIdValidator();
                validator.ValidateAndThrow(query);
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

                var validator = new DeleteBookCommandValidator();
                validator.ValidateAndThrow(command);
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
