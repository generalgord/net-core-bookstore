using WebApi.DBOperations;
using WebApi.Entities;

namespace WebApi.Repositories
{
    public class BookAuthorRepository
    {
        public BookAuthorRepository() { }

        public static void Initialize()
        {
            using (var context = new BookStoreDbContext())
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
                        }
                    );
                }

                context.BookAuthors.AddRange(bookAuthors);
                var added = context.SaveChanges();
                Console.WriteLine($"Added {added} Book Authors to DB");
            }
        }
    }
}
