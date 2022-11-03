using WebApi.DBOperations;
using WebApi.Entities;

namespace WebApi.UnitTests.TestSetup
{
    public static class RepoExtensions
    {
        public static void AddMockAuthors(this IBookStoreDbContext context)
        {
            if (context.Authors.Any())
                return;
            context.Authors.AddRange(TestSampleRepos.Authors);
        }

        public static void AddMockBooks(this IBookStoreDbContext context)
        {
            if (context.Books.Any())
                return;
            context.Books.AddRange(TestSampleRepos.Books);
        }

        public static void AddMockGenres(this IBookStoreDbContext context)
        {
            if (context.Genres.Any())
                return;
            context.Genres.AddRange(TestSampleRepos.Genres);
        }

        public static void AddMockBookAuthors(this IBookStoreDbContext context)
        {
            if (context.BookAuthors.Any())
                return;

            var books = context.Books.ToList();
            var authors = context.Authors.ToList();

            var bookAuthors = new List<BookAuthor>();
            foreach (var _book in books)
            {
                bookAuthors.Add(
                    new BookAuthor
                    {
                        Book = _book,
                        Author = authors.ElementAt(new Random().Next(authors.Count()))
                        // BookId = _book.Id,
                        // AuthorId = authors.ElementAt(new Random().Next(authors.Count())).Id
                    }
                );
            }

            context.BookAuthors.AddRange(bookAuthors);
        }
    }
}
