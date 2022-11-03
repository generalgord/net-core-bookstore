using WebApi.Operations.AuthorOperations.Create.Commands;

namespace WebApi.UnitTests.Operations.AuthorOperations.Commands.Create
{
    public class CreateAuthorCommandValidatorTests : IClassFixture<CommonTestFixture>
    {
        [Theory]
        [InlineData("", "")]
        [InlineData("", " ")]
        [InlineData(" ", "")]
        [InlineData(" ", " ")]
        [InlineData("Lo", "")]
        [InlineData("", "Me")]
        public void WhenInvalidInputsAreGiven_Validator_ShouldBeReturnErros(
            string firstName,
            string lastName
        )
        {
            // arrange
            var model = new CreateAuthorModel()
            {
                FirstName = firstName,
                LastName = lastName,
                DateOfBirth = new DateTime(1990, 10, 10),
            };
            CreateAuthorCommand command = new CreateAuthorCommand(null, null, model);

            CreateAuthorCommandValidator validator = new CreateAuthorCommandValidator();
            var results = validator.Validate(command);

            //assert
            results.Errors.Count.Should().BeGreaterThan(0);
        }

        [Fact]
        public void WhenDateTimeEqualNowIsGiven_Validator_ShouldBeReturnError()
        {
            var model = new CreateAuthorModel()
            {
                FirstName = "WhenDateTimeEqualNowIsGiven 1",
                LastName = "Validator_ShouldBeReturnError 32",
                DateOfBirth = DateTime.Now.Date
            };
            CreateAuthorCommand command = new CreateAuthorCommand(null, null, model);

            CreateAuthorCommandValidator validator = new CreateAuthorCommandValidator();
            var results = validator.Validate(command);

            results.Errors.Count.Should().BeGreaterThan(0);
        }

        [Fact]
        public void WhenValidInputAreGiven_Validator_ShouldNotBeReturnError()
        {
            var model = new CreateAuthorModel()
            {
                FirstName = "WhenValidInputAreGiven 324",
                LastName = "Validator_ShouldNotBeReturnError 43",
                DateOfBirth = DateTime.Now.AddYears(-18)
            };
            CreateAuthorCommand command = new CreateAuthorCommand(null, null, model);

            CreateAuthorCommandValidator validator = new CreateAuthorCommandValidator();
            var results = validator.Validate(command);

            results.Errors.Count.Should().Be(0);
        }
    }
}
