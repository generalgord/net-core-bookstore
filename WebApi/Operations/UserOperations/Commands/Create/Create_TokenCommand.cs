using AutoMapper;
using WebApi.DBOperations;
using WebApi.Entities;
using WebApi.TokenOperations;
using WebApi.TokenOperations.Models;

namespace WebApi.Operations.UserOperations.Create.Commands
{
    public class CreateTokenCommand
    {
        public CreateTokenModel Model { get; set; }
        private readonly IBookStoreDbContext _dbContext;
        private readonly IMapper _mapper;
        private readonly IConfiguration _config;

        public CreateTokenCommand(
            IBookStoreDbContext dbContext,
            IMapper mapper,
            IConfiguration config,
            CreateTokenModel model
        )
        {
            _dbContext = dbContext;
            Model = model;
            _mapper = mapper;
            _config = config;
        }

        public Token Handle()
        {
            var user = _dbContext.Users.FirstOrDefault(
                s => s.Email == Model.Email && s.Password == Model.Password
            );
            if (user is null)
                throw new AppException("The username or password is incorrect.");

            user = _mapper.Map<User>(Model);

            CustomTokenHandler tokenHandler = new CustomTokenHandler(_config);
            var token = tokenHandler.CreateAccessToken(user);

            user.RefreshToken = token.RefreshToken;
            user.RefreshTokenExpireDate = token.Expiration.GetValueOrDefault().AddMinutes(5);

            _dbContext.Users.Add(user);
            var isAdded = _dbContext.SaveChanges();
            if (isAdded <= 0)
                throw new AppException("An error occured while adding the user.");

            return token;
        }
    }

    public class CreateTokenModel
    {
        public string Email { get; set; } = "";
        public string Password { get; set; } = "";
    }
}
