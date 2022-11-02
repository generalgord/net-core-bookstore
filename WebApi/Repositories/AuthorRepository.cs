using WebApi.DBOperations;
using WebApi.Repositories.Initialize;

namespace WebApi.Repositories
{
    public class AuthorRepository
    {
        public AuthorRepository() { }

        public static void Initialize()
        {
            using (var context = new BookStoreDbContext())
            {
                if (context.Authors.Any())
                    return;

                context.Authors.AddRange(SampleRepos.Authors);
                var added = context.SaveChanges();
                Console.WriteLine($"Added {added} Authors to DB");
            }
        }
    }
}
