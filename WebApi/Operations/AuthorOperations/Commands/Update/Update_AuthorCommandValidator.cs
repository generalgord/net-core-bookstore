using FluentValidation;

namespace WebApi.Operations.AuthorOperations.Update.Commands
{
    public class UpdateAuthorCommandValidator : AbstractValidator<UpdateAuthorCommand>
    {
        public UpdateAuthorCommandValidator()
        {
            RuleFor(cmd => cmd.ID).GreaterThan(0);
            RuleFor(cmd => cmd.Model.FirstName).NotEmpty().MinimumLength(2);
            RuleFor(cmd => cmd.Model.LastName).NotEmpty().MinimumLength(2);
            RuleFor(cmd => cmd.Model.DateOfBirth).NotEmpty().LessThan(DateTime.Now.AddYears(-15));
            RuleForEach(cmd => cmd.Model.Books)
                .Where(w => w != null)
                .SetValidator(new AddBookToAuthorModelValidator());
        }
    }
}
