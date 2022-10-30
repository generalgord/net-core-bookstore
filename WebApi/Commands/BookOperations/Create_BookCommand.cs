using WebApi.Common;
using WebApi.DBOperations;
using WebApi.Entities;

namespace WebApi.Commands.BookOperations
{
    public class CreateBookCommand
    {
        public CreateBookModel Model { get; set; }
        readonly BookStoreDbContext _dbContext;

        public CreateBookCommand(BookStoreDbContext dbContext, CreateBookModel model)
        {
            _dbContext = dbContext;
            Model = model;
        }

        public void Handle()
        {
            var book = _dbContext.Books.SingleOrDefault(s => s.Title == Model.Title);
            if (book is not null)
                throw new AppException("Book already added");

            book = new Book();
            book.Title = Model.Title;
            book.PublishDate = Model.PublishDate;
            book.GenreId = Model.GenreId;
            book.PageCount = Model.PageCount;

            _dbContext.Books.Add(book);
            var isAdded = _dbContext.SaveChanges();
            if (isAdded <= 0)
                throw new AppException("An error occured while adding the book.");
        }
    }

    public class CreateBookModel
    {
        public string Title { get; set; } = "";
        public int GenreId { get; set; }
        public int PageCount { get; set; }
        public DateTime PublishDate { get; set; }
    }
}
