using WebApi.DBOperations;
using WebApi.Entities;
using WebApi.Operations.AuthorOperations.Delete.Commands;

namespace WebApi.UnitTests.Operations.AuthorOperations.Commands.Delete
{
    public class DeleteAuthorCommandTests : IClassFixture<CommonTestFixture>
    {
        readonly BookStoreDbContext _context;

        public DeleteAuthorCommandTests(CommonTestFixture testFixture)
        {
            _context = testFixture.Context;
        }

        [Fact]
        public void WhenAuthorNotFoundInDatabase_AppException_ShouldBeReturn()
        {
            var bookId = 0;

            DeleteAuthorCommand command = new DeleteAuthorCommand(_context, bookId);

            FluentActions
                .Invoking(() => command.Handle())
                .Should()
                .Throw<AppException>()
                .And.Message.Should()
                .Be("Author not found");
        }

        [Fact]
        public void WhenAuthorExistInDatabase_Author_ShouldBeDeleted()
        {
            var book = new Author()
            {
                FirstName = "WhenAuthorExistInDatabase_Author_ShouldBeDeleted FirstName",
                LastName = "WhenAuthorExistInDatabase_Author_ShouldBeDeleted LastName",
                DateOfBirth = new DateTime(1990, 10, 10),
            };
            _context.Authors.Add(book);
            _context.SaveChanges();

            DeleteAuthorCommand command = new DeleteAuthorCommand(_context, book.Id);
            FluentActions.Invoking(() => command.Handle()).Invoke();

            var bookCheck = _context.Authors.SingleOrDefault(s => s.Id == book.Id);
            bookCheck.Should().BeNull();
        }

        [Fact]
        public void WhenAuthorExistInDatabase_AuthorAuthors_ShouldBeDeleted()
        {
            var book = new Author()
            {
                FirstName = "WhenAuthorExistInDatabase_AuthorAuthors_ShouldBeDeleted 1214",
                LastName = "WhenAuthorExistInDatabase_AuthorAuthors_ShouldBeDeleted 141242",
                DateOfBirth = new DateTime(1990, 10, 10),
            };
            _context.Authors.Add(book);
            _context.SaveChanges();

            DeleteAuthorCommand command = new DeleteAuthorCommand(_context, book.Id);
            FluentActions.Invoking(() => command.Handle()).Invoke();

            var bookAuthorsCheck = _context.BookAuthors.SingleOrDefault(s => s.AuthorId == book.Id);
            bookAuthorsCheck.Should().BeNull();
        }
    }
}
