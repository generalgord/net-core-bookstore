using WebApi.Operations.BookOperations.Queries;

namespace WebApi.UnitTests.Operations.BookOperations.Queries
{
    public class QueryGetBookByIdValidatorTests : IClassFixture<CommonTestFixture>
    {
        [Fact]
        public void WhenInvalidInputsAreGiven_Validator_ShouldBeReturnErrors()
        {
            var itemId = 0;

            QueryGetBookById command = new QueryGetBookById(null, null, itemId);
            QueryGetBookByIdValidator validator = new QueryGetBookByIdValidator();
            var results = validator.Validate(command);

            results.Errors.Count.Should().BeGreaterThan(0);
        }

        [Fact]
        public void WhenValidInputsAreGiven_Validator_ShouldNotBeReturnErrors()
        {
            var itemId = 1;

            QueryGetBookById command = new QueryGetBookById(null, null, itemId);
            QueryGetBookByIdValidator validator = new QueryGetBookByIdValidator();
            var results = validator.Validate(command);

            results.Errors.Count.Should().Be(0);
        }
    }
}
