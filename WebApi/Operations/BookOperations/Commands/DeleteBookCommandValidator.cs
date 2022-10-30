using FluentValidation;
using WebApi.DBOperations;

namespace WebApi.Operations.BookOperations.Commands
{
    public class DeleteBookCommandValidator : AbstractValidator<DeleteBookCommand>
    {
        public DeleteBookCommandValidator()
        {
            RuleFor(cmd => cmd.ID).GreaterThan(0);
        }
    }
}
