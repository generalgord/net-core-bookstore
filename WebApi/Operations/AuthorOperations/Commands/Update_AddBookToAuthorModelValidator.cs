using FluentValidation;

namespace WebApi.Operations.AuthorOperations.Commands
{
    public class AddBookToAuthorModelValidator : AbstractValidator<AddBookToAuthorModel>
    {
        public AddBookToAuthorModelValidator()
        {
            RuleFor(cmd => cmd.GenreId).GreaterThan(0);
            RuleFor(cmd => cmd.PageCount).GreaterThan(0);
            RuleFor(cmd => cmd.PublishDate).NotEmpty();
            RuleFor(cmd => cmd.Title).MinimumLength(4).When(w => !string.IsNullOrEmpty(w.Title));
        }
    }
}
