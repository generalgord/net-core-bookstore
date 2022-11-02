using FluentValidation;
using WebApi.DBOperations;

namespace WebApi.Operations.GenreOperations.Delete.Commands
{
    public class DeleteGenreCommandValidator : AbstractValidator<DeleteGenreCommand>
    {
        public DeleteGenreCommandValidator()
        {
            RuleFor(cmd => cmd.ID).GreaterThan(0);
        }
    }
}
