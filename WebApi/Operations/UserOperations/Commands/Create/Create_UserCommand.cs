using AutoMapper;
using WebApi.DBOperations;
using WebApi.Entities;

namespace WebApi.Operations.UserOperations.Create.Commands
{
    public class CreateUserCommand
    {
        public CreateUserModel Model { get; set; }
        private readonly IBookStoreDbContext _dbContext;
        private readonly IMapper _mapper;

        public CreateUserCommand(
            IBookStoreDbContext dbContext,
            IMapper mapper,
            CreateUserModel model
        )
        {
            _dbContext = dbContext;
            Model = model;
            _mapper = mapper;
        }

        public void Handle()
        {
            var user = _dbContext.Users.SingleOrDefault(s => s.Email == Model.Email);
            if (user is not null)
                throw new AppException("User already added");

            user = _mapper.Map<User>(Model);

            _dbContext.Users.Add(user);
            var isAdded = _dbContext.SaveChanges();
            if (isAdded <= 0)
                throw new AppException("An error occured while adding the user.");
        }
    }

    public class CreateUserModel
    {
        public string FirstName { get; set; } = "";
        public string LastName { get; set; } = "";
        public string Email { get; set; } = "";
        public string Password { get; set; } = "";
        public bool IsActive { get; set; } = true;
    }
}
