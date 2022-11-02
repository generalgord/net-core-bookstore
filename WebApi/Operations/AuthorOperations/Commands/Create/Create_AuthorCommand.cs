using AutoMapper;
using WebApi.DBOperations;
using WebApi.Entities;

namespace WebApi.Operations.AuthorOperations.Create.Commands
{
    public class CreateAuthorCommand
    {
        public CreateAuthorModel Model { get; set; }
        private readonly IBookStoreDbContext _dbContext;
        private readonly IMapper _mapper;

        public CreateAuthorCommand(
            IBookStoreDbContext dbContext,
            IMapper mapper,
            CreateAuthorModel model
        )
        {
            _dbContext = dbContext;
            Model = model;
            _mapper = mapper;
        }

        public void Handle()
        {
            var author = _dbContext.Authors.SingleOrDefault(
                s => s.FirstName == Model.FirstName && s.LastName == Model.LastName
            );
            if (author is not null)
                throw new AppException("Author already added");

            author = _mapper.Map<Author>(Model);

            _dbContext.Authors.Add(author);
            var isAdded = _dbContext.SaveChanges();
            if (isAdded <= 0)
                throw new AppException("An error occured while adding the author.");
        }
    }

    public class CreateAuthorModel
    {
        public string FirstName { get; set; } = "";
        public string LastName { get; set; } = "";
        public DateTime DateOfBirth { get; set; }
    }
}
