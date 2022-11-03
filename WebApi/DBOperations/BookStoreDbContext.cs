using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using WebApi.Entities;

namespace WebApi.DBOperations
{
    public partial class BookStoreDbContext : DbContext, IBookStoreDbContext
    {
        public BookStoreDbContext(DbContextOptions<BookStoreDbContext> options) : base(options) { }

        public BookStoreDbContext() { }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseInMemoryDatabase(databaseName: "BookStoreDb");
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Author> Authors { get; set; }
        public DbSet<Book> Books { get; set; }
        public DbSet<BookAuthor> BookAuthors { get; set; }
        public DbSet<Genre> Genres { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Book<->Author bağlantısı
            // Bir kitabın birden fazla yazarı olabilir
            // Ve tabiki bir yazarın birden fazla kitabı olabilir
            // şeklinde kurgulanmıştır.
            modelBuilder.Entity<BookAuthor>().HasKey(ba => new { ba.BookId, ba.AuthorId });

            //// In the below code; For different key namings (e.g. BId, AId)
            //// Otherwise above code is sufficent.
            // // For Book
            // modelBuilder
            //     .Entity<BookAuthor>()
            //     .HasOne(ba => ba.Book)
            //     .WithMany(b => b.BookAuthors)
            //     .HasForeignKey(ba => ba.BId);
            // //For Author
            // modelBuilder
            //     .Entity<BookAuthor>()
            //     .HasOne(ba => ba.Author)
            //     .WithMany(b => b.BookAuthors)
            //     .HasForeignKey(ba => ba.AId);
        }

        public override int SaveChanges()
        {
            return base.SaveChanges();
        }

        public override EntityEntry Remove(object entity)
        {
            return base.Remove(entity);
        }

        public override void RemoveRange(params object[] entities)
        {
            base.RemoveRange(entities);
        }
    }
}
