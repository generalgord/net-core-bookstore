using AutoMapper;
using Microsoft.EntityFrameworkCore;
using WebApi.Common;
using WebApi.DBOperations;
using WebApi.Entities;

namespace WebApi.Operations.AuthorOperations.Queries
{
    public class QueryGetAuthors
    {
        readonly IBookStoreDbContext _dbContext;
        private readonly IMapper _mapper;

        public QueryGetAuthors(IBookStoreDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public List<AuthorsViewModel> Handle()
        {
            var bookList = (
                from a in _dbContext.Authors
                join ba in _dbContext.BookAuthors on a.Id equals ba.AuthorId into baGroup
                select new AuthorsViewModel
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
                                new AuthorsBooksViewModel
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
            ).ToList();

            return bookList;
        }
    }

    public class AuthorsViewModel
    {
        public int Id { get; set; }
        public string AuthorName { get; set; } = "";
        public string DateOfBirth { get; set; } = "";
        public List<AuthorsBooksViewModel>? Books { get; set; } = null;
    }

    public class AuthorsBooksViewModel
    {
        public int Id { get; set; }
        public string Title { get; set; } = "";
        public string Genre { get; set; } = "";
        public int PageCount { get; set; }
        public string PublishDate { get; set; } = "";
    }
}
