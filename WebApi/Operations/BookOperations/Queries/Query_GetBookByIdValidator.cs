using FluentValidation;

namespace WebApi.Operations.BookOperations.Queries
{
    public class QueryGetBookByIdValidator : AbstractValidator<QueryGetBookById>
    {
        public QueryGetBookByIdValidator()
        {
            RuleFor(cmd => cmd.ID).GreaterThan(0);
        }
    }
}
