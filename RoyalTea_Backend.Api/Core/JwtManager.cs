using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using RoyalTea_Backend.DataAccess;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace RoyalTea_Backend.Api.Core
{
    public class JwtManager
    {
        private AppDbContext dbContext;
        private JwtConfiguration jwtConfig;

        public JwtManager(AppDbContext dbContext, JwtConfiguration jwtConfig)
        {
            this.dbContext = dbContext;
            this.jwtConfig = jwtConfig;
        }

        public string CreateToken(string username, string password)
        {
            var user = dbContext.Users.Include(x => x.UseCases).FirstOrDefault(x => x.Username == username);

            if (user == null)
            {
                throw new UnauthorizedAccessException();
            }

            var isValidPw = BCrypt.Net.BCrypt.Verify(password, user.Password);

            if (!isValidPw)
            {
                throw new UnauthorizedAccessException();
            }

            var claims = Mapper.Map<List<Claim>>(user);

            var privateKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtConfig.PrivateKey));
            var credentials = new SigningCredentials(privateKey, SecurityAlgorithms.HmacSha256);
            var now = DateTime.UtcNow;
            var token = new JwtSecurityToken(
                issuer: jwtConfig.Issuer,
                audience: "Any",
                claims: claims,
                notBefore: now,
                expires: now.AddMinutes(jwtConfig.Duration),
                signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
