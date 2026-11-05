using core.Entities;

namespace core.Interfaces
{
    public interface IUserRepository
    {
        void AddUser(UserItem user);
        UserItem? GetByEmail(string email);
    }
}