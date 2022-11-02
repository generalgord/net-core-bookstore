using AutoMapper;
using Microsoft.EntityFrameworkCore;
using WebApi.Common;
using WebApi.DBOperations;
using WebApi.Entities;

namespace WebApi.Operations.BookOperations.Queries
{
    public class QueryGetBooks
    {
        readonly IBookStoreDbContext _dbContext;
        private readonly IMapper _mapper;

        public QueryGetBooks(IBookStoreDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public List<BooksViewModel> Handle()
        {
            var bookList = (
                from b in _dbContext.Books.Where(w => w.IsPublished)
                join ba in _dbContext.BookAuthors on b.Id equals ba.BookId into baGroup
                select new BooksViewModel
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
                                new BooksAuthorsViewModel
                                {
                                    AuthorName = sm.FirstName + ' ' + sm.LastName,
                                    DateOfBirth = sm.DateOfBirth.Date.ToString("dd/MM/yyyy")
                                }
                        )
                        .ToList()
                }
            ).ToList();

            return bookList;
        }
    }

    public class BooksViewModel
    {
        public int Id { get; set; }
        public string Title { get; set; } = "";
        public string Genre { get; set; } = "";
        public int PageCount { get; set; }
        public string PublishDate { get; set; } = "";
        public List<BooksAuthorsViewModel> Authors { get; set; } = null!;
    }

    public class BooksAuthorsViewModel
    {
        public string AuthorName { get; set; } = "";
        public string DateOfBirth { get; set; } = "";
    }
}
