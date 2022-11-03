using AutoMapper;
using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApi.DBOperations;
using WebApi.Operations.AuthorOperations.Create.Commands;
using WebApi.Operations.AuthorOperations.Delete.Commands;
using WebApi.Operations.AuthorOperations.Queries;
using WebApi.Operations.AuthorOperations.Update.Commands;

namespace WebApi.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class AuthorsController : ControllerBase
    {
        readonly IBookStoreDbContext _context;
        readonly IMapper _mapper;

        public AuthorsController(IBookStoreDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            QueryGetAuthors query = new QueryGetAuthors(_context, _mapper);
            var items = query.Handle();

            return Ok(items);
        }

        [HttpPost]
        public IActionResult AddAuthor([FromBody] CreateAuthorModel newAuthor)
        {
            CreateAuthorCommand command = new CreateAuthorCommand(_context, _mapper, newAuthor);

            var validator = new CreateAuthorCommandValidator();
            validator.ValidateAndThrow(command);
            command.Handle();

            return Ok();
        }

        [HttpPut("{id}")]
        public IActionResult UpdateAuthor(int id, [FromBody] UpdateAuthorModel updatedAuthor)
        {
            UpdateAuthorCommand command = new UpdateAuthorCommand(
                _context,
                _mapper,
                id,
                updatedAuthor
            );
            var validator = new UpdateAuthorCommandValidator();
            validator.ValidateAndThrow(command);
            command.Handle();

            return Ok();
        }

        [HttpGet("{id}")]
        public IActionResult GetAuthorById(int id)
        {
            QueryGetAuthorById query = new QueryGetAuthorById(_context, _mapper, id);

            var validator = new QueryGetAuthorByIdValidator();
            validator.ValidateAndThrow(query);
            var item = query.Handle();

            return Ok(item);
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteAuthor(int id)
        {
            DeleteAuthorCommand command = new DeleteAuthorCommand(_context, id);

            var validator = new DeleteAuthorCommandValidator();
            validator.ValidateAndThrow(command);
            command.Handle();

            return Ok();
        }
    }
}
