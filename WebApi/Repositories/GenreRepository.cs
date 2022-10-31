using WebApi.DBOperations;
using WebApi.Entities;

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

                var genres = new List<Genre>
                {
                    new Genre { Name = "Personal Growth" },
                    new Genre { Name = "Science Fiction" },
                    new Genre { Name = "Noval" },
                    new Genre { Name = "Romance" },
                    new Genre { Name = "Detective" },
                    new Genre { Name = "Classics" },
                    new Genre { Name = "Adventure" },
                    new Genre { Name = "Science" },
                    new Genre { Name = "Family" },
                };
                context.Genres.AddRange(genres);
                context.SaveChanges();
            }
        }
    }
}
