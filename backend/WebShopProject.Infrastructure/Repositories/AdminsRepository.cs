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
    internal class AdminsRepository(ProjectDbContext dbContext) : IAdminsRepository
    {
        private readonly static int iterations = 1000;
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
        public bool AdminWithCredentialsExists(string username, string password)
        {
            var admin = dbContext.Admins.FirstOrDefault(a => a.Username == username);

            if (admin == null)
            {
                return false;
            }

            if (VerifyPassword(password, admin.Password, admin.Salt))
            {
                return true;
            }
            return false;
        }

        public async  Task<Guid> CreateAdmin(Admin admin)
        {
            var (hashedPassword, salt) = HashPassword(admin.Password);
            admin.Password = hashedPassword;
            admin.Salt = salt;
            dbContext.Admins.Add(admin);
            await dbContext.SaveChangesAsync();
            return admin.AdminId;
        }

        public async Task DeleteAdmin(Admin admin)
        {
            dbContext.Remove(admin);
            await dbContext.SaveChangesAsync();
        }

        public async Task<Admin?> GetAdminById(Guid adminId)
        {
            var admin = await dbContext.Admins.FirstOrDefaultAsync(a => a.AdminId == adminId);
            return admin;
        }

        public async Task<Admin?> GetAdminByUsername(string username)
        {
            var admin = await dbContext.Admins
                .FirstOrDefaultAsync(x => x.Username == username);
            return admin;
        }

        public async Task<IEnumerable<Admin>> GetAllAdmins()
        {
            var admins = await dbContext.Admins.ToListAsync();
            return admins;
        }

        public async Task SaveChanges()
        {
            await dbContext.SaveChangesAsync();
        }
    }
}
