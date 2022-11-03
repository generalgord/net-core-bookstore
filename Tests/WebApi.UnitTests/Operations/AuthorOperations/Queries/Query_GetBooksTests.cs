using AutoMapper;
using WebApi.DBOperations;
using WebApi.Entities;
using WebApi.Operations.AuthorOperations.Queries;

namespace WebApi.UnitTests.Operations.AuthorOperations.Queries
{
    public class QueryGetAuthorsTests : IClassFixture<CommonTestFixture>
    {
        readonly BookStoreDbContext _context;
        readonly IMapper _mapper;

        public QueryGetAuthorsTests(CommonTestFixture testFixture)
        {
            _context = testFixture.Context;
            _mapper = testFixture.Mapper;
        }

        [Fact]
        public void WhenCalled_Authors_ShouldBeReturn()
        {
            var bookList = new List<Author>
            {
                new Author()
                {
                    FirstName = "WhenCalled_Authors_ShouldBeReturn FirstName 1",
                    LastName = "WhenCalled_Authors_ShouldBeReturn LastName 1",
                    DateOfBirth = new DateTime(1990, 10, 10),
                },
                new Author()
                {
                    FirstName = "WhenCalled_Authors_ShouldBeReturn FirstName 2",
                    LastName = "WhenCalled_Authors_ShouldBeReturn LastName 2",
                    DateOfBirth = new DateTime(1990, 10, 10),
                }
            };
            _context.Authors.AddRange(bookList);
            _context.SaveChanges();

            QueryGetAuthors query = new QueryGetAuthors(_context, _mapper);
            var results = FluentActions.Invoking(() => query.Handle()).Invoke();

            results.Should().NotBeNull();
            results.Count.Should().BeGreaterThanOrEqualTo(2); // With/Without Sample Test Repo Inserts
        }
    }
}
