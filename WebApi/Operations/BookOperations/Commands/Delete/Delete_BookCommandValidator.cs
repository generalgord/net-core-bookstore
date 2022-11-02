using FluentValidation;

namespace WebApi.Operations.BookOperations.Delete.Commands
{
    public class DeleteBookCommandValidator : AbstractValidator<DeleteBookCommand>
    {
        public DeleteBookCommandValidator()
        {
            RuleFor(cmd => cmd.ID).GreaterThan(0);
        }
    }
}
