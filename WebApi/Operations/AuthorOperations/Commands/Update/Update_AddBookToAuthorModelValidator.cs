using FluentValidation;

namespace WebApi.Operations.AuthorOperations.Update.Commands
{
    public class AddBookToAuthorModelValidator : AbstractValidator<AddBookToAuthorModel>
    {
        public AddBookToAuthorModelValidator()
        {
            RuleFor(cmd => cmd.GenreId).GreaterThan(0);
            RuleFor(cmd => cmd.PageCount).GreaterThan(0);
            RuleFor(cmd => cmd.PublishDate).NotEmpty().LessThan(DateTime.Now.Date);
            RuleFor(cmd => cmd.Title).NotEmpty().MinimumLength(4);
        }
    }
}
