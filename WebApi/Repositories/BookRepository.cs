using WebApi.DBOperations;
using WebApi.Repositories.Initialize;

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

                context.Books.AddRange(SampleRepos.Books);
                var added = context.SaveChanges();
                Console.WriteLine($"Added {added} Books to DB");
            }
        }
    }
}
