using FluentValidation;

namespace WebApi.Operations.GenreOperations.Commands
{
    public class CreateGenreCommandValidator : AbstractValidator<CreateGenreCommand>
    {
        public CreateGenreCommandValidator()
        {
            RuleFor(cmd => cmd.Model.Name).NotEmpty().MinimumLength(2);
        }
    }
}
