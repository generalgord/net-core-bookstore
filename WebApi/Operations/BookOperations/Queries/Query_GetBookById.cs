using AutoMapper;
using Microsoft.EntityFrameworkCore;
using WebApi.DBOperations;
using WebApi.Entities;

namespace WebApi.Operations.BookOperations.Queries
{
    public class QueryGetBookById
    {
        public int ID { get; set; }
        readonly BookStoreDbContext _dbContext;
        private readonly IMapper _mapper;

        public QueryGetBookById(BookStoreDbContext dbContext, IMapper mapper, int itemId)
        {
            _dbContext = dbContext;
            _mapper = mapper;
            ID = itemId;
        }

        public BookDetailViewModel Handle()
        {
            var book = _dbContext.Books.Include(i => i.Genre).SingleOrDefault(s => s.Id == ID);
            if (book is null)
                throw new AppException("Book not found");

            var vm = _mapper.Map<Book, BookDetailViewModel>(book);
            return vm;
        }
    }

    public class BookDetailViewModel
    {
        public string Title { get; set; } = "";
        public string Genre { get; set; } = "";
        public int PageCount { get; set; }
        public string PublishDate { get; set; } = "";
    }
}
