using FluentValidation;

namespace WebApi.Operations.BookOperations.Update.Commands
{
    public class UpdateBookCommandValidator : AbstractValidator<UpdateBookCommand>
    {
        public UpdateBookCommandValidator()
        {
            RuleFor(cmd => cmd.ID).GreaterThan(0);
            RuleFor(cmd => cmd.Model.GenreId).GreaterThan(0);
            RuleFor(cmd => cmd.Model.PageCount).GreaterThan(0);
            RuleFor(cmd => cmd.Model.PublishDate).NotEmpty();
            RuleFor(cmd => cmd.Model.Title).NotEmpty().MinimumLength(4);
            RuleForEach(cmd => cmd.Model.Authors)
                .Where(w => w != null)
                .SetValidator(new AddAuthorToBookModelValidator());
        }
    }
}
