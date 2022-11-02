using WebApi.DBOperations;
using WebApi.Entities;

namespace WebApi.UnitTests.TestSetup
{
    public static class RepoExtensions
    {
        public static void AddMockAuthors(this IBookStoreDbContext context)
        {
            context.Authors.AddRange(TestSampleRepos.Authors);
        }

        public static void AddMockBooks(this IBookStoreDbContext context)
        {
            context.Books.AddRange(TestSampleRepos.Books);
        }

        public static void AddMockGenres(this IBookStoreDbContext context)
        {
            context.Genres.AddRange(TestSampleRepos.Genres);
        }

        public static void AddMockBookAuthors(this IBookStoreDbContext context)
        {
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
                    }
                );
            }

            context.BookAuthors.AddRange(bookAuthors);
        }
    }
}
