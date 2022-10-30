using FluentValidation;

namespace WebApi.Operations.BookOperations.Commands
{
    public class UpdateBookCommandValidator : AbstractValidator<UpdateBookCommand>
    {
        public UpdateBookCommandValidator()
        {
            RuleFor(cmd => cmd.ID).GreaterThan(0);
            RuleFor(cmd => cmd.Model.GenreId).GreaterThan(0);
            RuleFor(cmd => cmd.Model.PageCount).GreaterThan(0);
            RuleFor(cmd => cmd.Model.PublishDate).NotEmpty().LessThan(DateTime.Now.Date);
            RuleFor(cmd => cmd.Model.Title).NotEmpty().MinimumLength(4);
        }
    }
}
