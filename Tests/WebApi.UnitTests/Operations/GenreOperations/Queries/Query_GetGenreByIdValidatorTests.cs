using WebApi.Operations.GenreOperations.Queries;

namespace WebApi.UnitTests.Operations.GenreOperations.Queries
{
    public class QueryGetGenreByIdValidatorTests : IClassFixture<CommonTestFixture>
    {
        //   [Fact]
        //   public void WhenInvalidInputsAreGivenT_Validator_ShouldBeReturnErrors()
        //   {
        //       var itemId = 0;

        //       QueryGetGenreById command = new QueryGetGenreById(null, null, itemId);
        //       QueryGetGenreByIdValidator validator = new QueryGetGenreByIdValidator();
        //       var results = validator.Validate(command);

        //       results.Errors.Count.Should().BeGreaterThan(0);
        //   }

        //   [Fact]
        //   public void WhenValidInputsAreGivenT_Validator_ShouldNotBeReturnErrors()
        //   {
        //       var itemId = 1;

        //       QueryGetGenreById command = new QueryGetGenreById(null, null, itemId);
        //       QueryGetGenreByIdValidator validator = new QueryGetGenreByIdValidator();
        //       var results = validator.Validate(command);

        //       results.Errors.Count.Should().Be(0);
        //   }
    }
}
