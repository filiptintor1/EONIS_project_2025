using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebShop.Domain.Entities;

namespace WebShopProject.Domain.Repositories
{
    public interface IUsersRepository
    {
        Task<IEnumerable<User>> GetAllUsers();
        Task<User?> GetUserById(Guid userId);
        Task<User?> GetUserByUsername(string username);
        bool UserWithCredentialsExists(string username, string password);
        Task<Guid> CreateUser(User user);
        Task DeleteUser(User user);
        Task SaveChanges();
    }
}
