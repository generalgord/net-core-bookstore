using AutoMapper;
using WebApi.DBOperations;
using WebApi.Entities;
using WebApi.Operations.AuthorOperations.Update.Commands;

namespace WebApi.UnitTests.Operations.AuthorOperations.Update
{
    public class UpdateAuthorCommandTests : IClassFixture<CommonTestFixture>
    {
        readonly BookStoreDbContext _context;
        readonly IMapper _mapper;

        public UpdateAuthorCommandTests(CommonTestFixture testFixture)
        {
            _context = testFixture.Context;
            _mapper = testFixture.Mapper;
        }

        [Fact]
        public void WhenAuthorIsNotFound_AppException_ShouldBeReturn()
        {
            var itemId = 0;

            UpdateAuthorCommand command = new UpdateAuthorCommand(_context, _mapper, itemId, null);

            FluentActions
                .Invoking(() => command.Handle())
                .Should()
                .Throw<AppException>()
                .And.Message.Should()
                .Be("Author not found");
        }

        [Fact]
        public void WhenAlreadyExistAuthorTitleAndWrongIdIsGiven_AppException_ShouldBeReturn()
        {
            var wrongItemId = 1;
            var author = new Author()
            {
                FirstName = "When Already Exist Author Name",
                LastName = "When Already Exist Author Surname",
                DateOfBirth = new DateTime(1960, 10, 10)
            };
            _context.Authors.Add(author);
            _context.SaveChanges();

            UpdateAuthorModel model = new UpdateAuthorModel()
            {
                FirstName = author.FirstName,
                LastName = author.LastName
            };

            UpdateAuthorCommand command = new UpdateAuthorCommand(
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
                .Be("Same name of Author is available. Please try another author name.");
        }

        [Fact]
        public void WhenAlreadyExistAuthorNameAndSameIdIsGiven_AppException_ShouldBeReturn()
        {
            var author = new Author()
            {
                FirstName = "WhenAlreadyExistAuthorNameAndSameIdIsGiven",
                LastName = "AppException_ShouldBeReturn",
            };
            _context.Authors.Add(author);
            _context.SaveChanges();

            UpdateAuthorModel model = new UpdateAuthorModel()
            {
                FirstName = author.FirstName,
                LastName = author.LastName
            };

            UpdateAuthorCommand command = new UpdateAuthorCommand(
                _context,
                _mapper,
                author.Id,
                model
            );

            FluentActions.Invoking(() => command.Handle()).Invoke();

            var updateAuthor = _context.Authors.SingleOrDefault(
                s => s.FirstName == model.FirstName && s.LastName == model.LastName
            );
            updateAuthor.Should().NotBeNull();
        }

        [Fact]
        public void WhenValidInputAreGiven_Author_ShouldBreCreated()
        {
            var book = new Author()
            {
                FirstName = "WhenValidInputAreGiven q21412",
                LastName = "Author_ShouldBreCreated 3523rw",
                DateOfBirth = new DateTime(1970, 10, 10)
            };
            _context.Authors.Add(book);
            _context.SaveChanges();

            UpdateAuthorModel model = new UpdateAuthorModel()
            {
                FirstName = "WhenValidInputAreGiven 3232",
                LastName = "Author_ShouldBreCreated 123523r",
                DateOfBirth = new DateTime(1970, 10, 10)
            };

            UpdateAuthorCommand command = new UpdateAuthorCommand(
                _context,
                _mapper,
                book.Id,
                model
            );

            FluentActions.Invoking(() => command.Handle()).Invoke();

            var updateAuthor = _context.Authors.SingleOrDefault(
                s => s.FirstName == model.FirstName && s.LastName == model.LastName
            );
            updateAuthor.Should().NotBeNull();
            updateAuthor.FirstName.Should().Be(model.FirstName);
            updateAuthor.LastName.Should().Be(model.LastName);
            updateAuthor.DateOfBirth.Should().Be(model.DateOfBirth);
        }
    }
}
