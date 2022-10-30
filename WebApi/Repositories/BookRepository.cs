using WebApi.DBOperations;
using WebApi.Entities;

namespace WebApi.Repositories
{
    public class BookRepository : IBookRepository
    {
        public BookRepository()
        {
            using (var context = new BookStoreDbContext())
            {
                if (context.Books.Any())
                    return;

                var Books = new List<Book>
                {
                    new Book { Title = "Mastering C# 8.0" },
                    new Book { Title = "Entity Framework Tutorial" },
                    new Book { Title = "ASP.NET 4.0 Programming" },
                    new Book { Title = "Let us C" },
                    new Book { Title = "Let us C++" },
                    new Book { Title = "Let us C#" }
                };
                context.Books.AddRange(Books);
                context.SaveChanges();
            }
        }
    }
}
