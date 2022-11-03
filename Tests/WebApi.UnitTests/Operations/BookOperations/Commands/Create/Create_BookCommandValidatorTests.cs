using WebApi.Operations.BookOperations.Create.Commands;

namespace WebApi.UnitTests.Operations.BookOperations.Commands.Create
{
    public class CreateBookCommandValidatorTests : IClassFixture<CommonTestFixture>
    {
        [Theory]
        [InlineData("Lord of the Mine", 0, 0)]
        [InlineData("Lord of the Mine 1", 0, 1)]
        [InlineData("", 0, 0)]
        [InlineData("", 0, 1)]
        [InlineData("", 100, 1)]
        [InlineData("Lo", 1, 1)]
        [InlineData("As", 0, 1)]
        [InlineData("Lo", 1, 0)]
        [InlineData("Lord a", 0, 0)]
        [InlineData("Lord b", 0, 1)]
        [InlineData("Lord c", 100, 0)]
        [InlineData(" ", 100, 1)]
        public void WhenInvalidInputsAreGiven_Validator_ShouldBeReturnErros(
            string title,
            int pageCount,
            int genreId
        )
        {
            // arrange
            var model = new CreateBookModel()
            {
                Title = title,
                PageCount = pageCount,
                PublishDate = DateTime.Now.Date.AddYears(-4),
                GenreId = genreId
            };
            CreateBookCommand command = new CreateBookCommand(null, null, model);

            CreateBookCommandValidator validator = new CreateBookCommandValidator();
            var results = validator.Validate(command);

            //assert
            results.Errors.Count.Should().BeGreaterThan(0);
        }

        [Fact]
        public void WhenDateTimeEqualNowIsGiven_Validator_ShouldBeReturnError()
        {
            var model = new CreateBookModel()
            {
                Title = "Lord of the Lotus",
                PageCount = 400,
                PublishDate = DateTime.Now.Date,
                GenreId = 12
            };
            CreateBookCommand command = new CreateBookCommand(null, null, model);

            CreateBookCommandValidator validator = new CreateBookCommandValidator();
            var results = validator.Validate(command);

            results.Errors.Count.Should().BeGreaterThan(0);
        }

        [Fact]
        public void WhenValidInputAreGiven_Validator_ShouldNotBeReturnError()
        {
            var model = new CreateBookModel()
            {
                Title = "Lord of the Gotus",
                PageCount = 400,
                PublishDate = DateTime.Now.AddYears(-5),
                GenreId = 12
            };
            CreateBookCommand command = new CreateBookCommand(null, null, model);

            CreateBookCommandValidator validator = new CreateBookCommandValidator();
            var results = validator.Validate(command);

            results.Errors.Count.Should().Be(0);
        }
    }
}
