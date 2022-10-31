using AutoMapper;
using WebApi.DBOperations;

namespace WebApi.Operations.GenreOperations.Queries
{
    public class QueryGetGenres
    {
        readonly BookStoreDbContext _dbContext;
        private readonly IMapper _mapper;

        public QueryGetGenres(BookStoreDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public List<GenresViewModel> Handle()
        {
            var genreList = _dbContext.Genres.Where(w => w.IsActive).OrderBy(o => o.Id).ToList();
            var vm = _mapper.Map<List<GenresViewModel>>(genreList);
            return vm;
        }
    }

    public class GenresViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; } = "";
        public bool IsActive { get; set; }
    }
}
