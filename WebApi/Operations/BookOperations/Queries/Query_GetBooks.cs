using AutoMapper;
using WebApi.Common;
using WebApi.DBOperations;

namespace WebApi.Operations.BookOperations.Queries
{
    public class QueryGetBooks
    {
        readonly BookStoreDbContext _dbContext;
        private readonly IMapper _mapper;

        public QueryGetBooks(BookStoreDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public List<BooksViewModel> Handle()
        {
            var bookList = _dbContext.Books.OrderBy(o => o.Id).ToList();
            var vm = _mapper.Map<List<BooksViewModel>>(bookList);
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
