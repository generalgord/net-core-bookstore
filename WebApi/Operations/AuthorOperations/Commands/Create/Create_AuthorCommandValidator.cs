using FluentValidation;

namespace WebApi.Operations.AuthorOperations.Create.Commands
{
    public class CreateAuthorCommandValidator : AbstractValidator<CreateAuthorCommand>
    {
        public CreateAuthorCommandValidator()
        {
            RuleFor(cmd => cmd.Model.FirstName).NotEmpty().MinimumLength(2);
            RuleFor(cmd => cmd.Model.LastName).NotEmpty().MinimumLength(2);
            RuleFor(cmd => cmd.Model.DateOfBirth).NotEmpty().LessThan(DateTime.Now.AddYears(-15));
        }
    }
}
