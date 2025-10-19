using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using WebShop.Domain.Entities;
using WebShopProject.Domain.Repositories;
using WebShopProject.Infrastructure.Database;

namespace WebShopProject.Infrastructure.Repositories
{
    internal class UsersRepository(ProjectDbContext dbContext) : IUsersRepository
    {
        private readonly static int iterations = 1000;

        public async Task<Guid> CreateUser(User user)
        {
            var (hashedPassword, salt) = HashPassword(user.Password);
            user.Password = hashedPassword;
            user.Salt = salt;
            dbContext.Users.Add(user);
            await dbContext.SaveChangesAsync();
            return user.UserId;
        }

        public async Task DeleteUser(User user)
        {

            dbContext.Remove(user);
            await dbContext.SaveChangesAsync();

        }

        public async Task<IEnumerable<User>> GetAllUsers()
        {
            var users = await dbContext.Users
                .Include(u =>u.Orders).ToListAsync();
            return users;
        }

        public async Task<User?> GetUserByUsername(string username)
        {
            return await dbContext.Users
         .FirstOrDefaultAsync(u => u.Username == username);
        }

        public async Task<User?> GetUserById(Guid userId)
        {
            var user = await dbContext.Users
             .Include(x => x.Orders)
             .FirstOrDefaultAsync(x => x.UserId == userId);
            return user;
        }

        public async Task SaveChanges()
        {
            await dbContext.SaveChangesAsync();
        }

        public bool UserWithCredentialsExists(string username, string password)
        {
            User user = dbContext.Users.FirstOrDefault(u => u.Username == username);

            if (user == null)
            {
                return false;
            }

            if (VerifyPassword(password, user.Password, user.Salt))
            {
                return true;
            }
            return false;
        }

        private Tuple<string, string> HashPassword(string password)
        {
            var sBytes = new byte[password.Length];
            new RNGCryptoServiceProvider().GetNonZeroBytes(sBytes);
            var salt = Convert.ToBase64String(sBytes);

            var derivedBytes = new Rfc2898DeriveBytes(password, sBytes, iterations);

            return new Tuple<string, string>
            (
                Convert.ToBase64String(derivedBytes.GetBytes(256)),
                salt
            );
        }

        public bool VerifyPassword(string password, string savedHash, string savedSalt)
        {
            var saltBytes = Convert.FromBase64String(savedSalt);
            var rfc2898DeriveBytes = new Rfc2898DeriveBytes(password, saltBytes, iterations);
            if (Convert.ToBase64String(rfc2898DeriveBytes.GetBytes(256)) == savedHash)
            {
                return true;
            }
            return false;
        }
    }
}
