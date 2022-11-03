using AutoMapper;
using WebApi.DBOperations;
using WebApi.Entities;
using WebApi.Operations.BookOperations.Update.Commands;

namespace WebApi.UnitTests.Operations.BookOperations.Update
{
    public class UpdateBookCommandTests : IClassFixture<CommonTestFixture>
    {
        readonly BookStoreDbContext _context;
        readonly IMapper _mapper;

        public UpdateBookCommandTests(CommonTestFixture testFixture)
        {
            _context = testFixture.Context;
            _mapper = testFixture.Mapper;
        }

        [Fact]
        public void WhenBookIsNotFound_AppException_ShouldBeReturn()
        {
            var itemId = 0;

            UpdateBookCommand command = new UpdateBookCommand(_context, _mapper, itemId, null);

            FluentActions
                .Invoking(() => command.Handle())
                .Should()
                .Throw<AppException>()
                .And.Message.Should()
                .Be("Book not found");
        }

        [Fact]
        public void WhenAlreadyExistBookTitleAndWrongIdIsGiven_AppException_ShouldBeReturn()
        {
            var wrongItemId = 1;
            var book = new Book()
            {
                Title = "When Already Exist Book Title",
                PageCount = 500,
                PublishDate = new DateTime(2000, 10, 10),
                GenreId = 1,
            };
            _context.Books.Add(book);
            _context.SaveChanges();

            UpdateBookModel model = new UpdateBookModel() { Title = book.Title };

            UpdateBookCommand command = new UpdateBookCommand(
                _context,
                _mapper,
                wrongItemId,
                model
            );

            FluentActions
                .Invoking(() => command.Handle())
                .Should()
                .Throw<AppException>()
                .And.Message.Should()
                .Be("Same name of Book is available. Please try another book title.");
        }

        [Fact]
        public void WhenAlreadyExistBookTitleAndSameIdIsGiven_AppException_ShouldBeReturn()
        {
            var book = new Book()
            {
                Title = "When Already AS Exist Book Title 2",
                PageCount = 500,
                PublishDate = new DateTime(2000, 10, 10),
                GenreId = 1,
            };
            _context.Books.Add(book);
            _context.SaveChanges();

            UpdateBookModel model = new UpdateBookModel() { Title = book.Title };

            UpdateBookCommand command = new UpdateBookCommand(_context, _mapper, book.Id, model);

            FluentActions.Invoking(() => command.Handle()).Invoke();

            var updateBook = _context.Books.SingleOrDefault(s => s.Title == model.Title);
            updateBook.Should().NotBeNull();
        }

        [Fact]
        public void WhenValidInputAreGiven_Book_ShouldBreCreated()
        {
            var book = new Book()
            {
                Title = "When Already Exist Book 3",
                PageCount = 500,
                PublishDate = new DateTime(2000, 10, 10),
                GenreId = 1,
            };
            _context.Books.Add(book);
            _context.SaveChanges();

            UpdateBookModel model = new UpdateBookModel()
            {
                Title = "Adventure of Mario Brothers 212",
                PageCount = 50,
                PublishDate = new DateTime(2000, 10, 10),
                GenreId = 2,
            };

            UpdateBookCommand command = new UpdateBookCommand(_context, _mapper, book.Id, model);

            FluentActions.Invoking(() => command.Handle()).Invoke();

            var updateBook = _context.Books.SingleOrDefault(s => s.Title == model.Title);
            updateBook.Should().NotBeNull();
            updateBook.PageCount.Should().Be(model.PageCount);
            updateBook.PublishDate.Should().Be(model.PublishDate);
            updateBook.GenreId.Should().Be(model.GenreId);
        }
    }
}
