using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using WebApi.Entities;

namespace WebApi.DBOperations
{
    public interface IBookStoreDbContext
    {
        // Entities
        DbSet<User> Users { get; set; }
        DbSet<Author> Authors { get; set; }
        DbSet<Book> Books { get; set; }
        DbSet<BookAuthor> BookAuthors { get; set; }
        DbSet<Genre> Genres { get; set; }

        // EF Methods
        int SaveChanges();
        EntityEntry Remove(object entity);
        void RemoveRange(params object[] entities);
    }
}
