using FluentValidation;

namespace WebApi.Operations.BookOperations.Update.Commands
{
    public class AddAuthorToBookModelValidator : AbstractValidator<AddAuthorToBookModel>
    {
        public AddAuthorToBookModelValidator()
        {
            RuleFor(cmd => cmd.FirstName)
                .MinimumLength(2)
                .When(w => !string.IsNullOrEmpty(w.FirstName));
            RuleFor(cmd => cmd.LastName)
                .MinimumLength(2)
                .When(w => !string.IsNullOrEmpty(w.FirstName));
            RuleFor(cmd => cmd.DateOfBirth).NotEmpty();
        }
    }
}
