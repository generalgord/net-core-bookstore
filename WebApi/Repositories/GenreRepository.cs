using WebApi.DBOperations;
using WebApi.Repositories.Initialize;

namespace WebApi.Repositories
{
    public class GenreRepository
    {
        public GenreRepository() { }

        public static void Initialize()
        {
            using (var context = new BookStoreDbContext())
            {
                if (context.Genres.Any())
                    return;

                context.Genres.AddRange(SampleRepos.Genres);
                var added = context.SaveChanges();
                Console.WriteLine($"Added {added} Genres to DB");
            }
        }
    }
}
