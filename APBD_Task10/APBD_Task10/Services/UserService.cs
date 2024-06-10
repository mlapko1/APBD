using Microsoft.AspNetCore.Identity;
using APBD_Task10.Models;

namespace APBD_Task10.Services
{
    public class UserService : IUserService
    {
        private readonly List<User> _users = new List<User>();
        private readonly Dictionary<string, string> _refreshTokens = new Dictionary<string, string>();
        private readonly IPasswordHasher<User> _passwordHasher;

        public UserService(IPasswordHasher<User> passwordHasher)
        {
            _passwordHasher = passwordHasher;
        }

        public async Task RegisterAsync(RegisterModel model)
        {
            var user = new User { Username = model.Username };
            user.PasswordHash = _passwordHasher.HashPassword(user, model.Password);
            _users.Add(user);
        }

        public async Task<User> AuthenticateAsync(string username, string password)
        {
            var user = _users.SingleOrDefault(x => x.Username == username);
            if (user == null || _passwordHasher.VerifyHashedPassword(user, user.PasswordHash, password) == PasswordVerificationResult.Failed)
                return null;

            return user;
        }

        public async Task SaveRefreshTokenAsync(string username, string refreshToken)
        {
            _refreshTokens[refreshToken] = username;
        }

        public async Task<string> GetUsernameByRefreshTokenAsync(string refreshToken)
        {
            return _refreshTokens.TryGetValue(refreshToken, out var username) ? username : null;
        }
    }
}