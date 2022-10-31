using WebApi.DBOperations;
using WebApi.Entities;

namespace WebApi.Repositories
{
    public class BookRepository
    {
        public BookRepository() { }

        public static void Initialize()
        {
            using (var context = new BookStoreDbContext())
            {
                if (context.Books.Any())
                    return;

                var Books = new List<Book>
                {
                    new Book { GenreId = 1, Title = "Mastering C# 8.0" },
                    new Book { GenreId = 2, Title = "Entity Framework Tutorial" },
                    new Book { GenreId = 3, Title = "ASP.NET 4.0 Programming" },
                    new Book { GenreId = 4, Title = "Let us C" },
                    new Book { GenreId = 2, Title = "Let us C++" },
                    new Book { GenreId = 3, Title = "Let us C#" }
                };
                context.Books.AddRange(Books);
                context.SaveChanges();
            }
        }
    }
}
