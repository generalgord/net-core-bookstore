using AutoMapper;
using WebApi.DBOperations;
using WebApi.Entities;
using WebApi.Operations.GenreOperations.Update.Commands;

namespace WebApi.UnitTests.Operations.GenreOperations.Update
{
    public class UpdateGenreCommandTests : IClassFixture<CommonTestFixture>
    {
        readonly BookStoreDbContext _context;
        readonly IMapper _mapper;

        public UpdateGenreCommandTests(CommonTestFixture testFixture)
        {
            _context = testFixture.Context;
            _mapper = testFixture.Mapper;
        }

        [Fact]
        public void WhenGenreIsNotFound_AppException_ShouldBeReturn()
        {
            var itemId = 0;

            UpdateGenreCommand command = new UpdateGenreCommand(_context, itemId, null);

            FluentActions
                .Invoking(() => command.Handle())
                .Should()
                .Throw<AppException>()
                .And.Message.Should()
                .Be("Genre not found");
        }

        [Fact]
        public void WhenAlreadyExistGenreTitleAndWrongIdIsGiven_AppException_ShouldBeReturn()
        {
            var wrongItemId = 1;
            var genre = new Genre() { Name = "When Already Exist Genre Title", };
            _context.Genres.Add(genre);
            _context.SaveChanges();

            UpdateGenreModel model = new UpdateGenreModel() { Name = genre.Name };

            UpdateGenreCommand command = new UpdateGenreCommand(_context, wrongItemId, model);

            FluentActions
                .Invoking(() => command.Handle())
                .Should()
                .Throw<AppException>()
                .And.Message.Should()
                .Be("Same name of Genre is available. Please try another genre title.");
        }

        [Fact]
        public void WhenAlreadyExistGenreTitleAndSameIdIsGiven_AppException_ShouldBeReturn()
        {
            var genre = new Genre() { Name = "When Already AS Exist Genre Title 124122" };
            _context.Genres.Add(genre);
            _context.SaveChanges();

            UpdateGenreModel model = new UpdateGenreModel() { Name = genre.Name };

            UpdateGenreCommand command = new UpdateGenreCommand(_context, genre.Id, model);

            FluentActions.Invoking(() => command.Handle()).Invoke();

            var updateGenre = _context.Genres.SingleOrDefault(s => s.Name == model.Name);
            updateGenre.Should().NotBeNull();
        }

        [Fact]
        public void WhenValidInputAreGiven_Genre_ShouldBreCreated()
        {
            var genre = new Genre() { Name = "When Already Exist Genre 124123", };
            _context.Genres.Add(genre);
            _context.SaveChanges();

            UpdateGenreModel model = new UpdateGenreModel()
            {
                Name = "Adventure of Mario Brothers 21412212"
            };

            UpdateGenreCommand command = new UpdateGenreCommand(_context, genre.Id, model);

            FluentActions.Invoking(() => command.Handle()).Invoke();

            var updateGenre = _context.Genres.SingleOrDefault(s => s.Name == model.Name);
            updateGenre.Should().NotBeNull();
            updateGenre.Name.Should().Be(model.Name);
        }
    }
}
