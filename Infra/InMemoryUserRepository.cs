using core.Entities;
using core.Interfaces;

namespace Infra
{
    public class InMemoryUserRepository : IUserRepository
    {
        private readonly List<UserItem> _users = new List<UserItem>();

        public void AddUser(UserItem user)
        {
            user.Id = _users.Count + 1;
            _users.Add(user);
        }

        public UserItem? GetByEmail(string email)
        {
            return _users.FirstOrDefault(u => u.Email.Equals(email, StringComparison.OrdinalIgnoreCase));
        }
    }
}