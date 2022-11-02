using AutoMapper;
using WebApi.DBOperations;
using WebApi.Entities;

namespace WebApi.Operations.GenreOperations.Create.Commands
{
    public class CreateGenreCommand
    {
        public CreateGenreModel Model { get; set; }
        private readonly IBookStoreDbContext _dbContext;
        private readonly IMapper _mapper;

        public CreateGenreCommand(
            IBookStoreDbContext dbContext,
            IMapper mapper,
            CreateGenreModel model
        )
        {
            _dbContext = dbContext;
            Model = model;
            _mapper = mapper;
        }

        public void Handle()
        {
            var genre = _dbContext.Genres.SingleOrDefault(s => s.Name == Model.Name);
            if (genre is not null)
                throw new AppException("Genre already added");

            genre = _mapper.Map<Genre>(Model);

            _dbContext.Genres.Add(genre);
            var isAdded = _dbContext.SaveChanges();
            if (isAdded <= 0)
                throw new AppException("An error occured while adding the genre.");
        }
    }

    public class CreateGenreModel
    {
        public string Name { get; set; } = "";
        public bool IsActive { get; set; } = true;
    }
}
