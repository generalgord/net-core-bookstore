using WebApi.Operations.AuthorOperations.Delete.Commands;

namespace WebApi.UnitTests.Operations.AuthorOperations.Commands.Delete
{
    public class DeleteAuthorCommandValidatorTests : IClassFixture<CommonTestFixture>
    {
        [Fact]
        public void WhenInvalidInputIsGiven_Validator_ShouldBeReturnError()
        {
            var itemId = 0;
            DeleteAuthorCommand command = new DeleteAuthorCommand(null, itemId);

            DeleteAuthorCommandValidator validator = new DeleteAuthorCommandValidator();
            var results = validator.Validate(command);

            results.Errors.Count.Should().Be(1);
        }

        [Fact]
        public void WhenValidInputIsGiven_Validator_ShouldNotBeReturnError()
        {
            var itemId = 1;
            DeleteAuthorCommand command = new DeleteAuthorCommand(null, itemId);

            DeleteAuthorCommandValidator validator = new DeleteAuthorCommandValidator();
            var results = validator.Validate(command);

            results.Errors.Count.Should().Be(0);
        }
    }
}
