using FluentValidation;

namespace WebApi.Operations.AuthorOperations.Delete.Commands
{
    public class DeleteAuthorCommandValidator : AbstractValidator<DeleteAuthorCommand>
    {
        public DeleteAuthorCommandValidator()
        {
            RuleFor(cmd => cmd.ID).GreaterThan(0);
        }
    }
}
