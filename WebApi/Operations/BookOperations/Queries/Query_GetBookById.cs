using AutoMapper;
using WebApi.DBOperations;

namespace WebApi.Operations.BookOperations.Queries
{
    public class QueryGetBookById
    {
        public int ID { get; set; }
        readonly IBookStoreDbContext _dbContext;
        private readonly IMapper _mapper;

        public QueryGetBookById(IBookStoreDbContext dbContext, IMapper mapper, int itemId)
        {
            _dbContext = dbContext;
            _mapper = mapper;
            ID = itemId;
        }

        public BookDetailViewModel Handle()
        {
            var book = (
                from b in _dbContext.Books.Where(w => w.IsPublished && w.Id == ID)
                join ba in _dbContext.BookAuthors on b.Id equals ba.BookId into baGroup
                select new BookDetailViewModel
                {
                    Id = b.Id,
                    Title = b.Title,
                    PageCount = b.PageCount,
                    PublishDate = b.PublishDate.Date.ToString("dd/MM/yyyy"),
                    Genre =
                        _dbContext.Genres
                            .Where(f => f.Id == b.GenreId && f.IsActive)
                            .Select(s => s.Name)
                            .FirstOrDefault() ?? "",
                    Authors = _dbContext.Authors
                        .Where(w => baGroup.Select(s => s.AuthorId).ToArray().Contains(w.Id))
                        .Select(
                            sm =>
                                new BookAuthorsViewModel
                                {
                                    AuthorName = sm.FirstName + ' ' + sm.LastName,
                                    DateOfBirth = sm.DateOfBirth.Date.ToString("dd/MM/yyyy")
                                }
                        )
                        .ToList()
                }
            ).SingleOrDefault();

            if (book is null)
                throw new AppException("Book not found");

            return book;
        }
    }

    public class BookDetailViewModel
    {
        public int Id { get; set; }
        public string Title { get; set; } = "";
        public string Genre { get; set; } = "";
        public int PageCount { get; set; }
        public string PublishDate { get; set; } = "";
        public List<BookAuthorsViewModel> Authors { get; set; } = null!;
    }

    public class BookAuthorsViewModel
    {
        public string AuthorName { get; set; } = "";
        public string DateOfBirth { get; set; } = "";
    }
}
