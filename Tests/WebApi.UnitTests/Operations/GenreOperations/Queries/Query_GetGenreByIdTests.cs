using AutoMapper;
using WebApi.DBOperations;
using WebApi.Entities;
using WebApi.Operations.GenreOperations.Queries;

namespace WebApi.UnitTests.Operations.GenreOperations.Queries
{
    public class QueryGetGenreByIdTests : IClassFixture<CommonTestFixture>
    {
        readonly BookStoreDbContext _context;
        readonly IMapper _mapper;

        public QueryGetGenreByIdTests(CommonTestFixture testFixture)
        {
            _context = testFixture.Context;
            _mapper = testFixture.Mapper;
        }

        [Fact]
        public void WhenGenreIdIsWrongGiven_AppException_ShouldBeReturn()
        {
            var itemId = -1;

            QueryGetGenreById query = new QueryGetGenreById(_context, _mapper, itemId);
            FluentActions
                .Invoking(() => query.Handle())
                .Should()
                .Throw<AppException>()
                .And.Message.Should()
                .Be("Genre not found or not active");
        }

        [Fact]
        public void WhenGenreIdIsValidGiven_Genre_ShouldBeReturn()
        {
            var genre = new Genre()
            {
                Name = "WhenGenreIdIsValidGiven_Genre_ShouldBeReturn Name 1",
            };
            _context.Genres.Add(genre);
            _context.SaveChanges();

            QueryGetGenreById query = new QueryGetGenreById(_context, _mapper, genre.Id);
            var result = FluentActions.Invoking(() => query.Handle()).Invoke();

            result.Should().NotBeNull();
            result.Id.Should().Be(genre.Id);
            result.Name.Should().Be(genre.Name);
        }
    }
}
