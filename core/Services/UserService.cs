using core.Interfaces;
using core.Resources;

namespace core.Services
{
    public class UserService : IUserService
    {
        public async Task<UserReadResource> RegisterAsync(UserRegisterResource resource)
        {
            return await Task.FromResult(new UserReadResource
            {
                Username = resource.Username,
                Email = resource.Email
            });
        }
    }
}