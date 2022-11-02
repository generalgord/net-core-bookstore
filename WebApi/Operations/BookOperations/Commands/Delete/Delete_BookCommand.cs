using WebApi.DBOperations;

namespace WebApi.Operations.BookOperations.Delete.Commands
{
    public class DeleteBookCommand
    {
        public int ID { get; set; }
        readonly IBookStoreDbContext _dbContext;

        public DeleteBookCommand(IBookStoreDbContext dbContext, int itemId)
        {
            _dbContext = dbContext;
            ID = itemId;
        }

        public void Handle()
        {
            var book = _dbContext.Books.SingleOrDefault(s => s.Id == ID);
            if (book is null)
                throw new AppException("Book not found");

            var bookAuthorsCheckList = _dbContext.BookAuthors
                .Where(w => w.BookId == book.Id)
                .ToList();

            _dbContext.Remove(book);
            if (bookAuthorsCheckList is not null && bookAuthorsCheckList.Count > 0)
                _dbContext.RemoveRange(bookAuthorsCheckList);
            _dbContext.SaveChanges();
        }
    }
}
