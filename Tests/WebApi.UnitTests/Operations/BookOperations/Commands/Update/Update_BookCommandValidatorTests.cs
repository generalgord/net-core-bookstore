using System.Globalization;
using WebApi.Operations.BookOperations.Update.Commands;

namespace WebApi.UnitTests.Operations.BookOperations.Commands.Update
{
    public class UpdateBookCommandValidatorTests : IClassFixture<CommonTestFixture>
    {
        [Theory]
        [InlineData(0, "", 0, 0)]
        public void WhenInvalidInputsAreGiven_Validator_ShouldBeReturnErros(
            int itemId,
            string title,
            int pageCount,
            int genreId
        )
        {
            UpdateBookModel model = new UpdateBookModel()
            {
                Title = title,
                PageCount = pageCount,
                GenreId = genreId,
                PublishDate = DateTime.Now.AddYears(-20),
            };

            UpdateBookCommand command = new UpdateBookCommand(null, null, itemId, model);

            UpdateBookCommandValidator validator = new UpdateBookCommandValidator();
            var results = validator.Validate(command);

            results.Errors.Count.Should().BeGreaterThan(0);
        }

        [Fact]
        public void WhenDateTimeEqualNowIsGiven_Validator_ShouldBeReturnError()
        {
            var itemId = 0;
            UpdateBookModel model = new UpdateBookModel()
            {
                Title = "Lord of the Minus",
                PageCount = 120,
                GenreId = 1,
                PublishDate = DateTime.Now,
            };

            UpdateBookCommand command = new UpdateBookCommand(null, null, itemId, model);

            UpdateBookCommandValidator validator = new UpdateBookCommandValidator();
            var results = validator.Validate(command);

            results.Errors.Count.Should().BeGreaterThan(0);
        }

        [Fact]
        public void WhenValidInputAreGiven_Validator_ShouldNotBeReturnError()
        {
            var itemId = 1;
            UpdateBookModel model = new UpdateBookModel()
            {
                Title = "Lord of the Silus",
                PageCount = 120,
                GenreId = 1,
                PublishDate = DateTime.Now.AddYears(-20),
            };

            UpdateBookCommand command = new UpdateBookCommand(null, null, itemId, model);

            UpdateBookCommandValidator validator = new UpdateBookCommandValidator();
            var results = validator.Validate(command);

            results.Errors.Count.Should().Be(0);
        }
    }
}
