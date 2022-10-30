using WebApi.Common;
using WebApi.DBOperations;

namespace WebApi.Operations.BookOperations.Queries
{
    public class QueryGetBooks
    {
        readonly BookStoreDbContext _dbContext;

        public QueryGetBooks(BookStoreDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public List<BooksViewModel> Handle()
        {
            var bookList = _dbContext.Books.OrderBy(o => o.Id).ToList();
            var vm = new List<BooksViewModel>();
            foreach (var book in bookList)
            {
                vm.Add(
                    new BooksViewModel()
                    {
                        Id = book.Id,
                        Title = book.Title,
                        Genre = ((GenreEnum)book.GenreId).ToString(),
                        PublishDate = book.PublishDate.Date.ToString("dd/MM/yyyy"),
                        PageCount = book.PageCount
                    }
                );
            }
            return vm;
        }
    }

    public class BooksViewModel
    {
        public int Id { get; set; }
        public string Title { get; set; } = "";
        public string Genre { get; set; } = "";
        public int PageCount { get; set; }
        public string PublishDate { get; set; } = "";
    }
}
