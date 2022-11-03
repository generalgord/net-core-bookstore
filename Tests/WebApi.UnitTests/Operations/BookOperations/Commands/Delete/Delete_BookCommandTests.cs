using WebApi.DBOperations;
using WebApi.Entities;
using WebApi.Operations.BookOperations.Delete.Commands;

namespace WebApi.UnitTests.Operations.BookOperations.Commands.Delete
{
    public class DeleteBookCommandTests : IClassFixture<CommonTestFixture>
    {
        readonly BookStoreDbContext _context;

        public DeleteBookCommandTests(CommonTestFixture testFixture)
        {
            _context = testFixture.Context;
        }

        [Fact]
        public void WhenBookNotFoundInDatabase_AppException_ShouldBeReturn()
        {
            var bookId = 0;

            DeleteBookCommand command = new DeleteBookCommand(_context, bookId);

            FluentActions
                .Invoking(() => command.Handle())
                .Should()
                .Throw<AppException>()
                .And.Message.Should()
                .Be("Book not found");
        }

        [Fact]
        public void WhenBookExistInDatabase_Book_ShouldBeDeleted()
        {
            var book = new Book()
            {
                Title = "WhenBookExistInDatabase_Book_ShouldBeDeleted 33",
                PageCount = 350,
                PublishDate = new DateTime(2000, 10, 10),
                GenreId = 1,
            };
            _context.Books.Add(book);
            _context.SaveChanges();

            DeleteBookCommand command = new DeleteBookCommand(_context, book.Id);
            FluentActions.Invoking(() => command.Handle()).Invoke();

            var bookCheck = _context.Books.SingleOrDefault(s => s.Id == book.Id);
            bookCheck.Should().BeNull();
        }

        [Fact]
        public void WhenBookExistInDatabase_BookAuthors_ShouldBeDeleted()
        {
            var book = new Book()
            {
                Title = "WhenBookExistInDatabase_BookAuthors_ShouldBeDeleted 22",
                PageCount = 350,
                PublishDate = new DateTime(2000, 10, 10),
                GenreId = 1,
            };
            _context.Books.Add(book);
            _context.SaveChanges();

            DeleteBookCommand command = new DeleteBookCommand(_context, book.Id);
            FluentActions.Invoking(() => command.Handle()).Invoke();

            var bookAuthorsCheck = _context.BookAuthors.SingleOrDefault(s => s.BookId == book.Id);
            bookAuthorsCheck.Should().BeNull();
        }
    }
}
