using AutoMapper;
using Microsoft.EntityFrameworkCore;
using WebApi.DBOperations;
using WebApi.Entities;

namespace WebApi.Operations.AuthorOperations.Queries
{
    public class QueryGetAuthorById
    {
        public int ID { get; set; }
        readonly IBookStoreDbContext _dbContext;
        private readonly IMapper _mapper;

        public QueryGetAuthorById(IBookStoreDbContext dbContext, IMapper mapper, int itemId)
        {
            _dbContext = dbContext;
            _mapper = mapper;
            ID = itemId;
        }

        public AuthorDetailViewModel Handle()
        {
            var author = (
                from a in _dbContext.Authors.Where(w => w.Id == ID)
                join ba in _dbContext.BookAuthors on a.Id equals ba.AuthorId into baGroup
                select new AuthorDetailViewModel
                {
                    Id = a.Id,
                    AuthorName = a.FirstName + ' ' + a.LastName,
                    DateOfBirth = a.DateOfBirth.Date.ToString("dd/MM/yyyy"),
                    Books = _dbContext.Books
                        .Where(
                            w =>
                                w.IsPublished
                                && baGroup.Select(s => s.BookId).ToArray().Contains(w.Id)
                        )
                        .Select(
                            sm =>
                                new AuthorBooksViewModel
                                {
                                    Id = sm.Id,
                                    Title = sm.Title,
                                    PageCount = sm.PageCount,
                                    PublishDate = sm.PublishDate.Date.ToString("dd/MM/yyyy"),
                                    Genre =
                                        _dbContext.Genres
                                            .Where(f => f.Id == sm.GenreId && f.IsActive)
                                            .Select(s => s.Name)
                                            .FirstOrDefault() ?? "",
                                }
                        )
                        .ToList()
                }
            ).SingleOrDefault();

            if (author is null)
                throw new AppException("Author not found");

            return author;
        }
    }

    public class AuthorDetailViewModel
    {
        public int Id { get; set; }
        public string AuthorName { get; set; } = "";
        public string DateOfBirth { get; set; } = "";
        public List<AuthorBooksViewModel> Books { get; set; } = null!;
    }

    public class AuthorBooksViewModel
    {
        public int Id { get; set; }
        public string Title { get; set; } = "";
        public string Genre { get; set; } = "";
        public int PageCount { get; set; }
        public string PublishDate { get; set; } = "";
    }
}
