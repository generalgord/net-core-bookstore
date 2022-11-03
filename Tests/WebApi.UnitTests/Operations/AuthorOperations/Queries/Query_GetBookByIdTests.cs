using System;
using AutoMapper;
using WebApi.DBOperations;
using WebApi.Entities;
using WebApi.Operations.AuthorOperations.Queries;

namespace WebApi.UnitTests.Operations.AuthorOperations.Queries
{
    public class QueryGetAuthorByIdTests : IClassFixture<CommonTestFixture>
    {
        readonly BookStoreDbContext _context;
        readonly IMapper _mapper;

        public QueryGetAuthorByIdTests(CommonTestFixture testFixture)
        {
            _context = testFixture.Context;
            _mapper = testFixture.Mapper;
        }

        [Fact]
        public void WhenAuthorIdIsWrongGiven_AppException_ShouldBeReturn()
        {
            var book = new Author()
            {
                FirstName = "WhenAuthorIdIsWrongGiven_AppException_ShouldBeReturn FirstName",
                LastName = "WhenAuthorIdIsWrongGiven_AppException_ShouldBeReturn LastName",
                DateOfBirth = new DateTime(1990, 10, 10),
            };
            _context.Authors.Add(book);
            _context.SaveChanges();

            var itemId = book.Id + 5;

            QueryGetAuthorById query = new QueryGetAuthorById(_context, _mapper, itemId);
            FluentActions
                .Invoking(() => query.Handle())
                .Should()
                .Throw<AppException>()
                .And.Message.Should()
                .Be("Author not found");
        }

        [Fact]
        public void WhenAuthorIdIsValidGiven_Author_ShouldBeReturn()
        {
            var book = new Author()
            {
                FirstName = "WhenAuthorIdIsWrongGiven_AppException_ShouldBeReturn FirstName",
                LastName = "WhenAuthorIdIsWrongGiven_AppException_ShouldBeReturn LastName",
                DateOfBirth = new DateTime(1990, 10, 10),
            };
            _context.Authors.Add(book);
            _context.SaveChanges();

            QueryGetAuthorById query = new QueryGetAuthorById(_context, _mapper, book.Id);
            var result = FluentActions.Invoking(() => query.Handle()).Invoke();

            result.Should().NotBeNull();
            result.Id.Should().Be(book.Id);
            result.AuthorName.Should().Be($"{book.FirstName} {book.LastName}");
        }
    }
}
