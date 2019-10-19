using Backend_Web.Models;
using Backend_Web.Utils;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace Backend_Web.DAL.DAO_s
{
    public class TokenDAO
    {
        public string Login(string username, string password, int expireMinutes = 30)
        {
            UserDAO userDAO = new UserDAO();
            User user = userDAO.FindByName(username);
            if (user == null)
            {
                return null;
            }

            if (user.Password != password)
            {
                return user.Name;
            }
            else
            {
                byte[] symmetricKey = Convert.FromBase64String(SecretHandler.Secret);
                JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();

                DateTime now = DateTime.UtcNow;
                SecurityTokenDescriptor tokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = new ClaimsIdentity(new[] { new Claim(ClaimTypes.Name, username) }),
                    Expires = now.AddMinutes(Convert.ToInt32(expireMinutes)),
                    SigningCredentials = new SigningCredentials(
                        new SymmetricSecurityKey(symmetricKey),
                        SecurityAlgorithms.HmacSha256Signature)
                };

                SecurityToken stoken = tokenHandler.CreateToken(tokenDescriptor);
                string token = tokenHandler.WriteToken(stoken);

                return token;
            }
        }
    }
}