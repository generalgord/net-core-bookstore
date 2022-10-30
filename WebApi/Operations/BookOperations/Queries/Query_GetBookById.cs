using WebApi.Common;
using WebApi.DBOperations;

namespace WebApi.Operations.BookOperations.Queries
{
    public class QueryGetBookById
    {
        public int ID { get; set; }
        readonly BookStoreDbContext _dbContext;

        public QueryGetBookById(BookStoreDbContext dbContext, int itemId)
        {
            _dbContext = dbContext;
            ID = itemId;
        }

        public BookViewModel Handle()
        {
            var book = _dbContext.Books.SingleOrDefault(s => s.Id == ID);
            if (book is null)
                throw new AppException("Book not found");

            var vm = new BookViewModel()
            {
                Title = book.Title,
                Genre = ((GenreEnum)book.GenreId).ToString(),
                PublishDate = book.PublishDate.Date.ToString("dd/MM/yyyy"),
                PageCount = book.PageCount
            };
            return vm;
        }
    }

    public class BookViewModel
    {
        public string Title { get; set; } = "";
        public string Genre { get; set; } = "";
        public int PageCount { get; set; }
        public string PublishDate { get; set; } = "";
    }
}
