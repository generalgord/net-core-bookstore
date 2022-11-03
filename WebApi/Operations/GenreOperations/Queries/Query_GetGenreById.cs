using AutoMapper;
using WebApi.DBOperations;
using WebApi.Entities;

namespace WebApi.Operations.GenreOperations.Queries
{
    public class QueryGetGenreById
    {
        public int ID { get; set; }
        readonly IBookStoreDbContext _dbContext;
        private readonly IMapper _mapper;

        public QueryGetGenreById(IBookStoreDbContext dbContext, IMapper mapper, int itemId)
        {
            _dbContext = dbContext;
            _mapper = mapper;
            ID = itemId;
        }

        public GenreDetailViewModel Handle()
        {
            var genre = _dbContext.Genres.SingleOrDefault(s => s.IsActive && s.Id == ID);
            if (genre is null)
                throw new AppException("Genre not found or not active");

            var vm = _mapper.Map<Genre, GenreDetailViewModel>(genre);
            return vm;
        }
    }

    public class GenreDetailViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; } = "";
    }
}
