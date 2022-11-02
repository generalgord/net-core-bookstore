using WebApi.DBOperations;

namespace WebApi.Operations.GenreOperations.Update.Commands
{
    public class UpdateGenreCommand
    {
        public UpdateGenreModel Model { get; set; }
        public int ID { get; set; }
        readonly IBookStoreDbContext _dbContext;

        public UpdateGenreCommand(IBookStoreDbContext dbContext, int itemId, UpdateGenreModel model)
        {
            _dbContext = dbContext;
            ID = itemId;
            Model = model;
        }

        public void Handle()
        {
            var genre = _dbContext.Genres.SingleOrDefault(s => s.Id == ID);
            if (genre is null)
                throw new AppException("Genre not found");

            if (_dbContext.Genres.Any(a => a.Name.ToLower() == Model.Name.ToLower() && a.Id != ID))
                throw new AppException(
                    "Same name of Genre is available. Please try another genre title."
                );

            genre.Name = Model.Name.Trim() != default ? Model.Name : genre.Name;
            genre.IsActive =
                Model.IsActive != default ? Model.IsActive.GetValueOrDefault() : genre.IsActive;

            _dbContext.Genres.Update(genre);
            var isAdded = _dbContext.SaveChanges();
            if (isAdded <= 0)
                throw new AppException("An error occured while updating the genre.");
        }
    }

    public class UpdateGenreModel
    {
        public string Name { get; set; } = "";
        public bool? IsActive { get; set; } = null;
    }
}
