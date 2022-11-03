using AutoMapper;
using WebApi.DBOperations;
using WebApi.Entities;
using WebApi.Operations.BookOperations.Create.Commands;

namespace WebApi.UnitTests.Operations.BookOperations.Commands.Create
{
    public class CreateBookCommandTests : IClassFixture<CommonTestFixture>
    {
        readonly BookStoreDbContext _context;
        readonly IMapper _mapper;

        public CreateBookCommandTests(CommonTestFixture testFixture)
        {
            _context = testFixture.Context;
            _mapper = testFixture.Mapper;
        }

        [Fact]
        public void WhenAlreadyExistBookTitleIsGiven_AppException_ShouldBeReturn()
        {
            var book = new Book()
            {
                Title = "WhenAlreadyExistBookTitleIsGiven_AppException_ShouldBeReturn 1",
                PageCount = 500,
                PublishDate = new DateTime(2000, 10, 10),
                GenreId = 1,
            };
            _context.Books.Add(book);
            _context.SaveChanges();

            CreateBookModel model = new CreateBookModel() { Title = book.Title };

            CreateBookCommand command = new CreateBookCommand(_context, _mapper, model);

            FluentActions
                .Invoking(() => command.Handle())
                .Should()
                .Throw<AppException>()
                .And.Message.Should()
                .Be("Book already added");
        }

        [Fact]
        public void WhenValidInputAreGiven_Book_ShouldBreCreated()
        {
            CreateBookModel model = new CreateBookModel()
            {
                Title = "Adventure of Mario 11",
                PageCount = 50,
                PublishDate = new DateTime(2000, 10, 10),
                GenreId = 2,
            };

            CreateBookCommand command = new CreateBookCommand(_context, _mapper, model);

            FluentActions.Invoking(() => command.Handle()).Invoke();

            var book = _context.Books.SingleOrDefault(s => s.Title == model.Title);
            book.Should().NotBeNull();
            book.PageCount.Should().Be(model.PageCount);
            book.PublishDate.Should().Be(model.PublishDate);
            book.GenreId.Should().Be(model.GenreId);
        }
    }
}
