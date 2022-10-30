using Microsoft.EntityFrameworkCore;
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

        public List<Book> GetBooks()
        {
            using (var context = new BookStoreDbContext())
            {
                var list = context.Books.Include(a => a.Author).ToList();
                return list;
            }
        }

        public bool AddBook(Book item)
        {
            using (var context = new BookStoreDbContext())
            {
                var alreadyAdded = context.Books.SingleOrDefault(s => s.Title == item.Title);
                if (alreadyAdded is not null)
                    return true;
                context.Books.Add(item);
                var isAdded = context.SaveChanges();
                return isAdded > 0 ? true : false;
            }
        }

        public bool RemoveBook(int itemId)
        {
            using (var context = new BookStoreDbContext())
            {
                var item = context.Books.SingleOrDefault(s => s.Id == itemId);
                if (item is null)
                    return true;
                context.Books.Remove(item);
                var isAdded = context.SaveChanges();
                return isAdded > 0 ? true : false;
            }
        }

        public Book UpdateBook(int itemId, Book updatedItem)
        {
            using (var context = new BookStoreDbContext())
            {
                var item = context.Books.SingleOrDefault(s => s.Id == itemId);
                if (item is null)
                    throw new ApplicationException("Book not available.");

                item.Title = updatedItem.Title != default ? updatedItem.Title : item.Title;
                item.GenreId = updatedItem.GenreId != default ? updatedItem.GenreId : item.GenreId;
                item.PublishDate =
                    updatedItem.PublishDate != default ? updatedItem.PublishDate : item.PublishDate;
                item.PageCount =
                    updatedItem.PageCount != default ? updatedItem.PageCount : item.PageCount;
                return item;
            }
        }
    }
}
