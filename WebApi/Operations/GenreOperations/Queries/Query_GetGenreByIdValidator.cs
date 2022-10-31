using FluentValidation;

namespace WebApi.Operations.GenreOperations.Queries
{
    public class QueryGetGenreByIdValidator : AbstractValidator<QueryGetGenreById>
    {
        public QueryGetGenreByIdValidator()
        {
            RuleFor(cmd => cmd.ID).GreaterThan(0);
        }
    }
}
