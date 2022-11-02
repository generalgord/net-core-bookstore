using FluentValidation;

namespace WebApi.Operations.BookOperations.Create.Commands
{
    public class CreateBookCommandValidator : AbstractValidator<CreateBookCommand>
    {
        public CreateBookCommandValidator()
        {
            RuleFor(cmd => cmd.Model.GenreId).GreaterThan(0);
            RuleFor(cmd => cmd.Model.PageCount).GreaterThan(0);
            RuleFor(cmd => cmd.Model.PublishDate).NotEmpty();
            RuleFor(cmd => cmd.Model.Title)
                .MinimumLength(4)
                .When(w => !string.IsNullOrEmpty(w.Model.Title));
        }
    }
}
