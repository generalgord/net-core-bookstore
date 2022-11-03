using WebApi.Operations.BookOperations.Delete.Commands;

namespace WebApi.UnitTests.Operations.BookOperations.Commands.Delete
{
    public class DeleteBookCommandValidatorTests : IClassFixture<CommonTestFixture>
    {
        [Fact]
        public void WhenInvalidInputIsGiven_Validator_ShouldBeReturnError()
        {
            var itemId = 0;
            DeleteBookCommand command = new DeleteBookCommand(null, itemId);

            DeleteBookCommandValidator validator = new DeleteBookCommandValidator();
            var results = validator.Validate(command);

            results.Errors.Count.Should().Be(1);
        }

        [Fact]
        public void WhenValidInputIsGiven_Validator_ShouldNotBeReturnError()
        {
            var itemId = 1;
            DeleteBookCommand command = new DeleteBookCommand(null, itemId);

            DeleteBookCommandValidator validator = new DeleteBookCommandValidator();
            var results = validator.Validate(command);

            results.Errors.Count.Should().Be(0);
        }
    }
}
