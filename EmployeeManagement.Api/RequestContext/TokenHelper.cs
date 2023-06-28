using EmployeeManagement.Dal.Entities;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace EmployeeManagement.Bll.Services
{
    public static class TokenHelper
    {
        public static string CreateAccessToken(Employee employee)
        {
            var claims = new List<Claim>
            {
                new Claim("Id", employee.Id.ToString()),
                new Claim("Name", employee.Name.ToString())
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("super secret key"));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);
            var expires = DateTime.Now.AddMinutes(30);
            var notBefore = DateTime.Now;

            var token = new JwtSecurityToken(null, null, claims, notBefore, expires, creds);
            var tokenHandler = new JwtSecurityTokenHandler();
            return tokenHandler.WriteToken(token);
        }
    }
}
