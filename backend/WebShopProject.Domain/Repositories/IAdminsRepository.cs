using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebShop.Domain.Entities;

namespace WebShopProject.Domain.Repositories
{
    public interface IAdminsRepository
    {
        Task<IEnumerable<Admin>> GetAllAdmins();
        Task DeleteAdmin(Admin admin);
        Task SaveChanges();
        Task<Admin?> GetAdminByUsername(string username);
        bool AdminWithCredentialsExists(string username, string password);
        Task<Admin?> GetAdminById(Guid adminId);
        Task<Guid> CreateAdmin(Admin admin);
        

    }
}
