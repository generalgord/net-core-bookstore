using AutoMapper;
using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApi.DBOperations;
using WebApi.Operations.GenreOperations.Create.Commands;
using WebApi.Operations.GenreOperations.Delete.Commands;
using WebApi.Operations.GenreOperations.Queries;
using WebApi.Operations.GenreOperations.Update.Commands;

namespace WebApi.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class GenresController : ControllerBase
    {
        readonly IBookStoreDbContext _context;
        readonly IMapper _mapper;

        public GenresController(IBookStoreDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            QueryGetGenres query = new QueryGetGenres(_context, _mapper);
            var items = query.Handle();

            return Ok(items);
        }

        [HttpPost]
        public IActionResult AddGenre([FromBody] CreateGenreModel newGenre)
        {
            CreateGenreCommand command = new CreateGenreCommand(_context, _mapper, newGenre);

            var validator = new CreateGenreCommandValidator();
            validator.ValidateAndThrow(command);
            command.Handle();

            return Ok();
        }

        [HttpPut("{id}")]
        public IActionResult UpdateGenre(int id, [FromBody] UpdateGenreModel updatedGenre)
        {
            UpdateGenreCommand command = new UpdateGenreCommand(_context, id, updatedGenre);
            var validator = new UpdateGenreCommandValidator();
            validator.ValidateAndThrow(command);
            command.Handle();

            return Ok();
        }

        [HttpGet("{id}")]
        public IActionResult GetGenreById(int id)
        {
            QueryGetGenreById query = new QueryGetGenreById(_context, _mapper, id);

            var validator = new QueryGetGenreByIdValidator();
            validator.ValidateAndThrow(query);
            var item = query.Handle();

            return Ok(item);
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteGenre(int id)
        {
            DeleteGenreCommand command = new DeleteGenreCommand(_context, id);

            var validator = new DeleteGenreCommandValidator();
            validator.ValidateAndThrow(command);
            command.Handle();

            return Ok();
        }
    }
}
