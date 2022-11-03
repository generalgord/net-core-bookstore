using WebApi.Operations.GenreOperations.Delete.Commands;

namespace WebApi.UnitTests.Operations.GenreOperations.Commands.Delete
{
    public class DeleteGenreCommandValidatorTests : IClassFixture<CommonTestFixture>
    {
        [Fact]
        public void WhenInvalidInputIsGiven_Validator_ShouldBeReturnError()
        {
            var itemId = 0;
            DeleteGenreCommand command = new DeleteGenreCommand(null, itemId);

            DeleteGenreCommandValidator validator = new DeleteGenreCommandValidator();
            var results = validator.Validate(command);

            results.Errors.Count.Should().Be(1);
        }

        [Fact]
        public void WhenValidInputIsGiven_Validator_ShouldNotBeReturnError()
        {
            var itemId = 1;
            DeleteGenreCommand command = new DeleteGenreCommand(null, itemId);

            DeleteGenreCommandValidator validator = new DeleteGenreCommandValidator();
            var results = validator.Validate(command);

            results.Errors.Count.Should().Be(0);
        }
    }
}
