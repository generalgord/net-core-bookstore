using WebApi.Operations.AuthorOperations.Queries;

namespace WebApi.UnitTests.Operations.AuthorOperations.Queries
{
    public class QueryGetAuthorByIdValidatorTests : IClassFixture<CommonTestFixture>
    {
        [Fact]
        public void WhenInvalidInputsAreGiven_Validator_ShouldBeReturnErrors()
        {
            var itemId = 0;

            QueryGetAuthorById command = new QueryGetAuthorById(null, null, itemId);
            QueryGetAuthorByIdValidator validator = new QueryGetAuthorByIdValidator();
            var results = validator.Validate(command);

            results.Errors.Count.Should().BeGreaterThan(0);
        }

        [Fact]
        public void WhenValidInputsAreGiven_Validator_ShouldNotBeReturnErrors()
        {
            var itemId = 1;

            QueryGetAuthorById command = new QueryGetAuthorById(null, null, itemId);
            QueryGetAuthorByIdValidator validator = new QueryGetAuthorByIdValidator();
            var results = validator.Validate(command);

            results.Errors.Count.Should().Be(0);
        }
    }
}
