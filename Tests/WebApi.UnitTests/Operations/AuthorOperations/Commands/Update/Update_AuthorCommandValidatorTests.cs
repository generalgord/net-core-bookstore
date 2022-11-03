using WebApi.Operations.AuthorOperations.Update.Commands;

namespace WebApi.UnitTests.Operations.AuthorOperations.Commands.Update
{
    public class UpdateAuthorCommandValidatorTests : IClassFixture<CommonTestFixture>
    {
        [Theory]
        [InlineData(0, "", "")]
        [InlineData(0, "", " ")]
        [InlineData(0, " ", "")]
        [InlineData(0, "S", "")]
        [InlineData(0, "S", " ")]
        [InlineData(0, "", "S")]
        [InlineData(0, " ", "S")]
        [InlineData(1, "", "")]
        [InlineData(1, "", " ")]
        [InlineData(1, " ", "")]
        [InlineData(1, "S", "")]
        [InlineData(1, "S", " ")]
        [InlineData(1, "", "S")]
        [InlineData(1, " ", "S")]
        public void WhenInvalidInputsAreGiven_Validator_ShouldBeReturnErros(
            int itemId,
            string firstName,
            string lastName
        )
        {
            UpdateAuthorModel model = new UpdateAuthorModel()
            {
                FirstName = firstName,
                LastName = lastName,
                DateOfBirth = new DateTime(1970, 10, 10)
            };

            UpdateAuthorCommand command = new UpdateAuthorCommand(null, null, itemId, model);

            UpdateAuthorCommandValidator validator = new UpdateAuthorCommandValidator();
            var results = validator.Validate(command);

            results.Errors.Count.Should().BeGreaterThan(0);
        }

        [Fact]
        public void WhenDateTimeEqualNowIsGiven_Validator_ShouldBeReturnError()
        {
            var itemId = 0;
            UpdateAuthorModel model = new UpdateAuthorModel()
            {
                FirstName = "WhenValidInputAreGiven q21412",
                LastName = "Author_ShouldBreCreated 3523rw",
                DateOfBirth = DateTime.Now
            };

            UpdateAuthorCommand command = new UpdateAuthorCommand(null, null, itemId, model);

            UpdateAuthorCommandValidator validator = new UpdateAuthorCommandValidator();
            var results = validator.Validate(command);

            results.Errors.Count.Should().BeGreaterThan(0);
        }

        [Fact]
        public void WhenValidInputAreGiven_Validator_ShouldNotBeReturnError()
        {
            var itemId = 1;
            UpdateAuthorModel model = new UpdateAuthorModel()
            {
                FirstName = "WhenValidInputAreGiven q21412",
                LastName = "Author_ShouldBreCreated 3523rw",
                DateOfBirth = DateTime.Now.AddYears(-20),
            };

            UpdateAuthorCommand command = new UpdateAuthorCommand(null, null, itemId, model);

            UpdateAuthorCommandValidator validator = new UpdateAuthorCommandValidator();
            var results = validator.Validate(command);

            results.Errors.Count.Should().Be(0);
        }
    }
}
