using WebApi.Operations.GenreOperations.Create.Commands;

namespace WebApi.UnitTests.Operations.GenreOperations.Commands.Create
{
    public class CreateGenreCommandValidatorTests : IClassFixture<CommonTestFixture>
    {
        [Theory]
        [InlineData("")]
        [InlineData(" ")]
        [InlineData("G")]
        public void WhenInvalidInputsAreGiven_Validator_ShouldBeReturnErros(string name)
        {
            // arrange
            var model = new CreateGenreModel() { Name = name };
            CreateGenreCommand command = new CreateGenreCommand(null, null, model);

            CreateGenreCommandValidator validator = new CreateGenreCommandValidator();
            var results = validator.Validate(command);

            //assert
            results.Errors.Count.Should().BeGreaterThan(0);
        }

        [Fact]
        public void WhenValidInputAreGiven_Validator_ShouldNotBeReturnError()
        {
            var model = new CreateGenreModel() { Name = "Lord of the Gotus" };
            CreateGenreCommand command = new CreateGenreCommand(null, null, model);

            CreateGenreCommandValidator validator = new CreateGenreCommandValidator();
            var results = validator.Validate(command);

            results.Errors.Count.Should().Be(0);
        }
    }
}
