using APBD_Task10.Models;

namespace APBD_Task10.Services{
    public interface IUserService
    {
        Task RegisterAsync(RegisterModel model);
        Task<User> AuthenticateAsync(string username, string password);
        Task SaveRefreshTokenAsync(string username, string refreshToken);
        Task<string> GetUsernameByRefreshTokenAsync(string refreshToken);
    }
}