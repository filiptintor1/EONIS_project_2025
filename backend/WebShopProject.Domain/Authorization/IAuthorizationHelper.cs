using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebShopProject.Domain.Entities;

namespace WebShopProject.Domain.Authorization
{
    public interface IAuthorizationHelper
    {
        public bool Authenticate(Credentials creds);

        public bool IsRoleAdmin(Credentials creds);

        public string GenerateToken(Credentials creds);
    }
}
