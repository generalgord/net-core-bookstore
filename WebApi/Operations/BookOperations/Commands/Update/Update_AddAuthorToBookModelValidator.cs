using FluentValidation;

namespace WebApi.Operations.BookOperations.Update.Commands
{
    public class AddAuthorToBookModelValidator : AbstractValidator<AddAuthorToBookModel>
    {
        public AddAuthorToBookModelValidator()
        {
            RuleFor(cmd => cmd.FirstName).NotEmpty().MinimumLength(2);
            RuleFor(cmd => cmd.LastName).NotEmpty().MinimumLength(2);
            RuleFor(cmd => cmd.DateOfBirth).NotEmpty().LessThan(DateTime.Now.AddYears(-15).Date);
        }
    }
}
