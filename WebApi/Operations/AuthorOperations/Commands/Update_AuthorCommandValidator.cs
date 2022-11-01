using FluentValidation;
using WebApi.Operations.BookOperations.Commands;

namespace WebApi.Operations.AuthorOperations.Commands
{
    public class UpdateAuthorCommandValidator : AbstractValidator<UpdateAuthorCommand>
    {
        public UpdateAuthorCommandValidator()
        {
            RuleFor(cmd => cmd.ID).GreaterThan(0);
            RuleFor(cmd => cmd.Model.FirstName)
                .MinimumLength(2)
                .When(w => !string.IsNullOrEmpty(w.Model.FirstName));
            RuleFor(cmd => cmd.Model.LastName)
                .MinimumLength(2)
                .When(w => !string.IsNullOrEmpty(w.Model.FirstName));
            RuleFor(cmd => cmd.Model.DateOfBirth).NotEmpty();
            RuleForEach(cmd => cmd.Model.Books)
                .Where(w => w != null)
                .SetValidator(new AddBookToAuthorModelValidator());
        }
    }
}
