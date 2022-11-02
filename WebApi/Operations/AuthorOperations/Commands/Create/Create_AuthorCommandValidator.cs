using FluentValidation;

namespace WebApi.Operations.AuthorOperations.Create.Commands
{
    public class CreateAuthorCommandValidator : AbstractValidator<CreateAuthorCommand>
    {
        public CreateAuthorCommandValidator()
        {
            RuleFor(cmd => cmd.Model.FirstName)
                .MinimumLength(2)
                .When(w => !string.IsNullOrEmpty(w.Model.FirstName));
            RuleFor(cmd => cmd.Model.LastName)
                .MinimumLength(2)
                .When(w => !string.IsNullOrEmpty(w.Model.FirstName));
            RuleFor(cmd => cmd.Model.DateOfBirth).NotEmpty();
        }
    }
}
