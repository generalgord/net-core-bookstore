using WebApi.Operations.GenreOperations.Update.Commands;

namespace WebApi.UnitTests.Operations.GenreOperations.Commands.Update
{
    public class UpdateGenreCommandValidatorTests : IClassFixture<CommonTestFixture>
    {
        [Theory]
        [InlineData(0, "")]
        [InlineData(0, " ")]
        [InlineData(0, "G")]
        [InlineData(1, "")]
        [InlineData(1, " ")]
        [InlineData(1, "G")]
        public void WhenInvalidInputsAreGiven_Validator_ShouldBeReturnErros(int itemId, string name)
        {
            UpdateGenreModel model = new UpdateGenreModel() { Name = name };

            UpdateGenreCommand command = new UpdateGenreCommand(null, itemId, model);

            UpdateGenreCommandValidator validator = new UpdateGenreCommandValidator();
            var results = validator.Validate(command);

            results.Errors.Count.Should().BeGreaterThan(0);
        }

        [Fact]
        public void WhenValidInputAreGiven_Validator_ShouldNotBeReturnError()
        {
            var itemId = 1;
            UpdateGenreModel model = new UpdateGenreModel() { Name = "Lord of the Silus 6236" };

            UpdateGenreCommand command = new UpdateGenreCommand(null, itemId, model);

            UpdateGenreCommandValidator validator = new UpdateGenreCommandValidator();
            var results = validator.Validate(command);

            results.Errors.Count.Should().Be(0);
        }
    }
}
