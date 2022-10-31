using FluentValidation;

namespace WebApi.Operations.BookOperations.Commands
{
    public class UpdateBookCommandValidator : AbstractValidator<UpdateBookCommand>
    {
        public UpdateBookCommandValidator()
        {
            RuleFor(cmd => cmd.ID).GreaterThan(0);
            RuleFor(cmd => cmd.Model.GenreId).GreaterThan(0).IsInEnum();
            RuleFor(cmd => cmd.Model.PageCount).GreaterThan(0);
            RuleFor(cmd => cmd.Model.PublishDate).NotEmpty();
            RuleFor(cmd => cmd.Model.Title)
                .MinimumLength(4)
                .When(w => !string.IsNullOrEmpty(w.Model.Title));
        }
    }
}
