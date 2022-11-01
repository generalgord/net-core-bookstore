using AutoMapper;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using WebApi.DBOperations;
using WebApi.Operations.BookOperations.Commands;
using WebApi.Operations.BookOperations.Queries;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class BooksController : ControllerBase
    {
        readonly BookStoreDbContext _context;
        readonly IMapper _mapper;

        public BooksController(BookStoreDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            QueryGetBooks query = new QueryGetBooks(_context, _mapper);
            var items = query.Handle();

            return Ok(items);
        }

        [HttpPost]
        public IActionResult AddBook([FromBody] CreateBookModel newBook)
        {
            CreateBookCommand command = new CreateBookCommand(_context, _mapper, newBook);

            var validator = new CreateBookCommandValidator();
            validator.ValidateAndThrow(command);
            command.Handle();

            return Ok();
        }

        [HttpPut("{id}")]
        public IActionResult UpdateBook(int id, [FromBody] UpdateBookModel updatedBook)
        {
            UpdateBookCommand command = new UpdateBookCommand(_context, _mapper, id, updatedBook);
            var validator = new UpdateBookCommandValidator();
            validator.ValidateAndThrow(command);
            command.Handle();

            return Ok();
        }

        [HttpGet("{id}")]
        public IActionResult GetBookById(int id)
        {
            QueryGetBookById query = new QueryGetBookById(_context, _mapper, id);

            var validator = new QueryGetBookByIdValidator();
            validator.ValidateAndThrow(query);
            var item = query.Handle();

            return Ok(item);
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteBook(int id)
        {
            DeleteBookCommand command = new DeleteBookCommand(_context, id);

            var validator = new DeleteBookCommandValidator();
            validator.ValidateAndThrow(command);
            command.Handle();

            return Ok();
        }
    }
}
