using WebApi.Common;
using WebApi.DBOperations;

namespace WebApi.Commands.BookOperations
{
    public class DeleteBookCommand
    {
        public int ID { get; set; }
        readonly BookStoreDbContext _dbContext;

        public DeleteBookCommand(BookStoreDbContext dbContext, int itemId)
        {
            _dbContext = dbContext;
            ID = itemId;
        }

        public void Handle()
        {
            var book = _dbContext.Books.SingleOrDefault(s => s.Id == ID);
            if (book is null)
                throw new AppException("Book not found");

            _dbContext.Remove(book);
            _dbContext.SaveChanges();
        }
    }
}
