using Microsoft.EntityFrameworkCore;
using WebApi.DBOperations;
using WebApi.Entities;

namespace WebApi.Repositories
{
    public class AuthorRepository : IAuthorRepository
    {
        public AuthorRepository()
        {
            Initialize();
        }

        public static void Initialize()
        {
            using (var context = new BookStoreDbContext())
            {
                if (context.Authors.Any())
                    return;

                var authors = new List<Author>
                {
                    new Author
                    {
                        FirstName = "Joydip",
                        LastName = "Kanjilal",
                        Books = new List<Book>()
                        {
                            new Book { Title = "Mastering C# 8.0" },
                            new Book { Title = "Entity Framework Tutorial" },
                            new Book { Title = "ASP.NET 4.0 Programming" }
                        }
                    },
                    new Author
                    {
                        FirstName = "Yashavanth",
                        LastName = "Kanetkar",
                        Books = new List<Book>()
                        {
                            new Book { Title = "Let us C" },
                            new Book { Title = "Let us C++" },
                            new Book { Title = "Let us C#" }
                        }
                    }
                };
                context.Authors.AddRange(authors);
                context.SaveChanges();
            }
        }

        public List<Author> GetAuthors()
        {
            using (var context = new BookStoreDbContext())
            {
                var list = context.Authors.Include(a => a.Books).ToList();
                return list;
            }
        }

        public bool AddAuthor(Author item)
        {
            using (var context = new BookStoreDbContext())
            {
                var alreadyAdded = context.Authors.SingleOrDefault(
                    s => s.FirstName == item.FirstName && s.LastName == item.LastName
                );
                if (alreadyAdded is not null)
                    return true;
                context.Authors.Add(item);
                var isAdded = context.SaveChanges();
                return isAdded > 0 ? true : false;
            }
        }

        public bool RemoveAuthor(int itemId)
        {
            using (var context = new BookStoreDbContext())
            {
                var item = context.Authors.SingleOrDefault(s => s.Id == itemId);
                if (item is null)
                    return true;
                context.Authors.Remove(item);
                var isAdded = context.SaveChanges();
                return isAdded > 0 ? true : false;
            }
        }

        public Author UpdateAuthor(int itemId, Author updatedItem)
        {
            using (var context = new BookStoreDbContext())
            {
                var item = context.Authors.SingleOrDefault(s => s.Id == itemId);
                if (item is null)
                    throw new AppException("Author not available.");

                item.FirstName =
                    updatedItem.FirstName != default ? updatedItem.FirstName : item.FirstName;
                item.LastName =
                    updatedItem.LastName != default ? updatedItem.LastName : item.LastName;

                context.Authors.Update(item);
                context.SaveChanges();
                return item;
            }
        }
    }
}
