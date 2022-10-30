using WebApi.Common;
using WebApi.DBOperations;
using WebApi.Entities;

namespace WebApi.Commands.BookOperations
{
    public class UpdateBookCommand
    {
        public UpdateBookModel Model { get; set; }
        public int ID { get; set; }
        readonly BookStoreDbContext _dbContext;

        public UpdateBookCommand(BookStoreDbContext dbContext, int itemId, UpdateBookModel model)
        {
            _dbContext = dbContext;
            ID = itemId;
            Model = model;
        }

        public void Handle()
        {
            var book = _dbContext.Books.SingleOrDefault(s => s.Id == ID);
            if (book is null)
                throw new AppException("Book not found");

            book.Title = Model.Title;
            book.PublishDate = Model.PublishDate;
            book.GenreId = Model.GenreId;
            book.PageCount = Model.PageCount;

            _dbContext.Books.Update(book);
            var isAdded = _dbContext.SaveChanges();
            if (isAdded <= 0)
                throw new AppException("An error occured while updating the book.");
        }
    }

    public class UpdateBookModel
    {
        public string Title { get; set; } = "";
        public int GenreId { get; set; }
        public int PageCount { get; set; }
        public DateTime PublishDate { get; set; }
    }
}
