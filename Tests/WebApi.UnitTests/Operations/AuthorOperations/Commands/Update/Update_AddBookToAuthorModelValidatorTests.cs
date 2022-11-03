using WebApi.Operations.AuthorOperations.Update.Commands;
using WebApi.Operations.BookOperations.Update.Commands;

namespace WebApi.UnitTests.Operations.BookOperations.Commands.Update
{
    public class AddBookToAuthorModelValidatorTests : IClassFixture<CommonTestFixture>
    {
        [Theory]
        [InlineData(0, "", 0, 0)]
        [InlineData(0, " ", 0, 0)]
        [InlineData(0, "", 1, 0)]
        [InlineData(0, "", 0, 1)]
        [InlineData(0, " ", 1, 0)]
        [InlineData(0, " ", 0, 1)]
        [InlineData(0, "Lor", 0, 0)]
        [InlineData(1, "", 0, 0)]
        [InlineData(1, " ", 0, 0)]
        [InlineData(1, "", 1, 0)]
        [InlineData(1, "", 0, 1)]
        [InlineData(1, " ", 1, 0)]
        [InlineData(1, " ", 0, 1)]
        [InlineData(1, "Lor", 0, 0)]
        public void WhenInvalidInputsAreGiven_Validator_ShouldBeReturnErros(
            int itemId,
            string title,
            int genreId,
            int pageCount
        )
        {
            AddBookToAuthorModel commandBook = new AddBookToAuthorModel()
            {
                Id = itemId,
                Title = title,
                GenreId = genreId,
                PageCount = pageCount,
                PublishDate = DateTime.Now.AddYears(-20),
            };

            AddBookToAuthorModelValidator validator = new AddBookToAuthorModelValidator();
            var results = validator.Validate(commandBook);

            results.Errors.Count.Should().BeGreaterThan(0);
        }

        [Fact]
        public void WhenDateTimeEqualNowIsGiven_Validator_ShouldBeReturnError()
        {
            AddBookToAuthorModel commandBook = new AddBookToAuthorModel()
            {
                Id = 1,
                Title = "WhenDateTimeEqualNowIsGiven_Validator_ShouldBeReturnError 124124",
                GenreId = 1,
                PageCount = 124,
                PublishDate = DateTime.Now.Date,
            };

            AddBookToAuthorModelValidator validator = new AddBookToAuthorModelValidator();
            var results = validator.Validate(commandBook);

            results.Errors.Count.Should().BeGreaterThan(0);
        }

        [Fact]
        public void WhenValidInputAreGiven_Validator_ShouldNotBeReturnError()
        {
            AddBookToAuthorModel commandBook = new AddBookToAuthorModel()
            {
                Id = 1,
                Title = "WhenValidInputAreGiven_Validator_ShouldNotBeReturnError 55235",
                GenreId = 1,
                PageCount = 124,
                PublishDate = DateTime.Now.AddYears(-20),
            };

            AddBookToAuthorModelValidator validator = new AddBookToAuthorModelValidator();
            var results = validator.Validate(commandBook);

            results.Errors.Count.Should().Be(0);
        }
    }
}
