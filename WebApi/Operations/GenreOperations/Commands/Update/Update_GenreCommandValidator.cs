using FluentValidation;

namespace WebApi.Operations.GenreOperations.Update.Commands
{
    public class UpdateGenreCommandValidator : AbstractValidator<UpdateGenreCommand>
    {
        public UpdateGenreCommandValidator()
        {
            RuleFor(cmd => cmd.ID).GreaterThan(0);
            RuleFor(cmd => cmd.Model.Name)
                .MinimumLength(2)
                .When(w => !string.IsNullOrEmpty(w.Model.Name));
        }
    }
}
