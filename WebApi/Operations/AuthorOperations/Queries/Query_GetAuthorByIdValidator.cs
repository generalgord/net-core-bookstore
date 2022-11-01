using FluentValidation;

namespace WebApi.Operations.AuthorOperations.Queries
{
    public class QueryGetAuthorByIdValidator : AbstractValidator<QueryGetAuthorById>
    {
        public QueryGetAuthorByIdValidator()
        {
            RuleFor(cmd => cmd.ID).GreaterThan(0);
        }
    }
}
