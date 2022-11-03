using AutoMapper;
using WebApi.DBOperations;
using WebApi.Entities;
using WebApi.Operations.GenreOperations.Queries;

namespace WebApi.UnitTests.Operations.GenreOperations.Queries
{
    public class QueryGetGenresTests : IClassFixture<CommonTestFixture>
    {
        readonly BookStoreDbContext _context;
        readonly IMapper _mapper;

        public QueryGetGenresTests(CommonTestFixture testFixture)
        {
            _context = testFixture.Context;
            _mapper = testFixture.Mapper;
        }

        [Fact]
        public void WhenCalled_Genres_ShouldBeReturn()
        {
            var genreList = new List<Genre>
            {
                new Genre() { Name = "WhenCalled 1131", },
                new Genre() { Name = "Genre_ShouldBeReturn 2122", }
            };
            _context.Genres.AddRange(genreList);
            _context.SaveChanges();

            QueryGetGenres query = new QueryGetGenres(_context, _mapper);
            var results = FluentActions.Invoking(() => query.Handle()).Invoke();

            results.Should().NotBeNull();
            results.Count.Should().BeGreaterThanOrEqualTo(2); // With/Without Sample Test Repo Inserts
        }
    }
}
