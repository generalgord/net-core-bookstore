using WebApi.DBOperations;
using WebApi.TokenOperations;
using WebApi.TokenOperations.Models;

namespace WebApi.Operations.UserOperations.Commands.Create
{
    public class RefreshTokenCommand
    {
        public string RefreshToken { get; set; }
        private readonly IBookStoreDbContext _dbContext;
        private readonly IConfiguration _config;

        public RefreshTokenCommand(
            IBookStoreDbContext dbContext,
            IConfiguration config,
            string refreshToken
        )
        {
            _dbContext = dbContext;
            _config = config;
            RefreshToken = refreshToken;
        }

        public Token Handle()
        {
            var user = _dbContext.Users.FirstOrDefault(
                s => s.RefreshToken == RefreshToken && s.RefreshTokenExpireDate > DateTime.Now
            );
            if (user is null)
                throw new AppException("The Renewal Key is invalid.");

            CustomTokenHandler tokenHandler = new CustomTokenHandler(_config);
            var token = tokenHandler.CreateAccessToken(user);

            user.RefreshToken = token.RefreshToken;
            user.RefreshTokenExpireDate = token.Expiration.GetValueOrDefault().AddMinutes(5);

            _dbContext.Users.Update(user);
            var isAdded = _dbContext.SaveChanges();
            if (isAdded <= 0)
                throw new AppException("An error occured while adding the user.");

            return token;
        }
    }
}
