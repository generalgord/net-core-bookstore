using System;
using AutoMapper;
using WebApi.DBOperations;
using WebApi.Entities;
using WebApi.Operations.BookOperations.Queries;

namespace WebApi.UnitTests.Operations.BookOperations.Queries
{
    public class QueryGetBookByIdTests : IClassFixture<CommonTestFixture>
    {
        readonly BookStoreDbContext _context;
        readonly IMapper _mapper;

        public QueryGetBookByIdTests(CommonTestFixture testFixture)
        {
            _context = testFixture.Context;
            _mapper = testFixture.Mapper;
        }

        [Fact]
        public void WhenBookIdIsWrongGiven_AppException_ShouldBeReturn()
        {
            var itemId = -1;

            QueryGetBookById query = new QueryGetBookById(_context, _mapper, itemId);
            FluentActions
                .Invoking(() => query.Handle())
                .Should()
                .Throw<AppException>()
                .And.Message.Should()
                .Be("Book not found");
        }

        [Fact]
        public void WhenBookIdIsValidGiven_Book_ShouldBeReturn()
        {
            var book = new Book()
            {
                Title = "WhenBookIdIs ValidGiven _Book_ShouldB eReturn 1",
                PageCount = 500,
                PublishDate = new DateTime(2000, 10, 10),
                GenreId = 1,
            };
            _context.Books.Add(book);
            _context.SaveChanges();

            QueryGetBookById query = new QueryGetBookById(_context, _mapper, book.Id);
            var result = FluentActions.Invoking(() => query.Handle()).Invoke();

            result.Should().NotBeNull();
            result.Id.Should().Be(book.Id);
            result.Title.Should().Be(book.Title);
        }
    }
}
