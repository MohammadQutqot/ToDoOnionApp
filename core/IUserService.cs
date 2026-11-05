using core.Resources;

namespace core.Interfaces
{
    public interface IUserService
    {
        Task<UserReadResource> RegisterAsync(UserRegisterResource resource);
    }
}