using WebApi.DBOperations;

namespace WebApi.Operations.GenreOperations.Delete.Commands
{
    public class DeleteGenreCommand
    {
        public int ID { get; set; }
        readonly IBookStoreDbContext _dbContext;

        public DeleteGenreCommand(IBookStoreDbContext dbContext, int itemId)
        {
            _dbContext = dbContext;
            ID = itemId;
        }

        public void Handle()
        {
            var genre = _dbContext.Genres.SingleOrDefault(s => s.Id == ID);
            if (genre is null)
                throw new AppException("Genre not found");

            _dbContext.Remove(genre);
            _dbContext.SaveChanges();
        }
    }
}
