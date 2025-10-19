using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using WebShopProject.Domain.Authorization;
using WebShopProject.Domain.Entities;
using WebShopProject.Domain.Repositories;
using WebShopProject.Infrastructure.Repositories;

namespace WebShopProject.Infrastructure.Authorization
{
    public class AuthorizationHelper(IUsersRepository usersRepository,
    IAdminsRepository adminsRepository) : IAuthorizationHelper
    {
        public bool Authenticate(Credentials creds)
        {
            if (usersRepository.UserWithCredentialsExists(creds.Username, creds.Password))
            {
                return true;
            }

            if (adminsRepository.AdminWithCredentialsExists(creds.Username, creds.Password))
            {
                return true;
            }

            return false;
        }

        public string GenerateToken(Credentials creds)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("1234567JWXcOW6yjJyz666WU+yxnXbRnbMWzQVB/Vqc="));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            string role = IsRoleAdmin(creds) ? "Admin" : "User";

            var claims = new[]
            {
                new Claim(ClaimTypes.Email, creds.Username),
                new Claim(ClaimTypes.Role, role)
            };

            var token = new JwtSecurityToken("ftn.uns.ac.rs",
                                             "ftn.uns.ac.rs",
                                             claims,
                                             expires: DateTime.Now.AddMinutes(120),
                                             signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        public bool IsRoleAdmin(Credentials creds)
        {
            if (adminsRepository.AdminWithCredentialsExists(creds.Username, creds.Password))
            {
                return true;
            }
            return false;
        }
    }
}
