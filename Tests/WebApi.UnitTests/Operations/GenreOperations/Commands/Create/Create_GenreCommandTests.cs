using AutoMapper;
using WebApi.DBOperations;
using WebApi.Entities;
using WebApi.Operations.GenreOperations.Create.Commands;

namespace WebApi.UnitTests.Operations.GenreOperations.Commands.Create
{
    public class CreateGenreCommandTests : IClassFixture<CommonTestFixture>
    {
        readonly BookStoreDbContext _context;
        readonly IMapper _mapper;

        public CreateGenreCommandTests(CommonTestFixture testFixture)
        {
            _context = testFixture.Context;
            _mapper = testFixture.Mapper;
        }

        [Fact]
        public void WhenAlreadyExistGenreTitleIsGiven_AppException_ShouldBeReturn()
        {
            var genre = new Genre() { Name = "New Genre Is Here", IsActive = true };
            _context.Genres.Add(genre);
            _context.SaveChanges();

            CreateGenreModel model = new CreateGenreModel() { Name = genre.Name };

            CreateGenreCommand command = new CreateGenreCommand(_context, _mapper, model);

            FluentActions
                .Invoking(() => command.Handle())
                .Should()
                .Throw<AppException>()
                .And.Message.Should()
                .Be("Genre already added");
        }

        [Fact]
        public void WhenValidInputAreGiven_Genre_ShouldBreCreated()
        {
            CreateGenreModel model = new CreateGenreModel() { Name = "Adventure of Mario 352351", };

            CreateGenreCommand command = new CreateGenreCommand(_context, _mapper, model);

            FluentActions.Invoking(() => command.Handle()).Invoke();

            var book = _context.Genres.SingleOrDefault(s => s.Name == model.Name);
            book.Should().NotBeNull();
            book.Name.Should().Be(model.Name);
        }
    }
}
