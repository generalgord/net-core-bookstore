using FluentValidation;

namespace WebApi.Operations.AuthorOperations.Commands
{
    public class DeleteAuthorCommandValidator : AbstractValidator<DeleteAuthorCommand>
    {
        public DeleteAuthorCommandValidator()
        {
            RuleFor(cmd => cmd.ID).GreaterThan(0);
        }
    }
}
