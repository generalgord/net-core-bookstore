using AutoMapper;
using WebApi.DBOperations;
using WebApi.Entities;
using WebApi.Operations.AuthorOperations.Create.Commands;

namespace WebApi.UnitTests.Operations.AuthorOperations.Commands.Create
{
    public class CreateAuthorCommandTests : IClassFixture<CommonTestFixture>
    {
        readonly BookStoreDbContext _context;
        readonly IMapper _mapper;

        public CreateAuthorCommandTests(CommonTestFixture testFixture)
        {
            _context = testFixture.Context;
            _mapper = testFixture.Mapper;
        }

        [Fact]
        public void WhenAlreadyExistAuthorIsGiven_AppException_ShouldBeReturn()
        {
            var author = new Author()
            {
                FirstName = "WhenAlreadyExistAuthorIsGiven_AppException_ShouldBeReturn FirstName",
                LastName = "WhenAlreadyExistAuthorIsGiven_AppException_ShouldBeReturn LastName",
                DateOfBirth = new DateTime(1990, 10, 10),
            };
            _context.Authors.Add(author);
            _context.SaveChanges();

            CreateAuthorModel model = new CreateAuthorModel()
            {
                FirstName = author.FirstName,
                LastName = author.LastName
            };

            CreateAuthorCommand command = new CreateAuthorCommand(_context, _mapper, model);

            FluentActions
                .Invoking(() => command.Handle())
                .Should()
                .Throw<AppException>()
                .And.Message.Should()
                .Be("Author already added");
        }

        [Fact]
        public void WhenValidInputAreGiven_Author_ShouldBreCreated()
        {
            CreateAuthorModel model = new CreateAuthorModel()
            {
                FirstName = "WhenValidInputAreGiven_Author_ShouldBreCreated FirstName 23",
                LastName = "WhenValidInputAreGiven_Author_ShouldBreCreated LastName 42",
                DateOfBirth = new DateTime(1990, 10, 10),
            };

            CreateAuthorCommand command = new CreateAuthorCommand(_context, _mapper, model);

            FluentActions.Invoking(() => command.Handle()).Invoke();

            var book = _context.Authors.SingleOrDefault(
                s => s.FirstName == model.FirstName && s.LastName == model.LastName
            );
            book.Should().NotBeNull();
            book.FirstName.Should().Be(model.FirstName);
            book.LastName.Should().Be(model.LastName);
            book.DateOfBirth.Should().Be(model.DateOfBirth);
        }
    }
}
