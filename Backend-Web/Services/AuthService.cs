using Backend_Web.DAL.DAO_s;
using Backend_Web.Utils;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace Backend_Web.Services
{
    public class AuthService
    {
        public BaseResponse<string> Login(string username, string password)
        {
            try
            {
                using (SHA256 mySHA256 = SHA256.Create())
                {
                    byte[] PasswordHash = mySHA256.ComputeHash(Encoding.ASCII.GetBytes(password));
                    password = Encoding.ASCII.GetString(PasswordHash);
                }

                TokenDAO tokenDAO = new TokenDAO();
                string token = tokenDAO.Login(username, password);

                if (string.IsNullOrEmpty(token) || token == username)
                {
                    return new BaseResponse<string> { Status = Status.ERROR, Message = "User and password don't match" };
                }
                else
                {
                    return new BaseResponse<string> { Status = Status.OK, Content = token, Message = "Success" };
                }
                //if (token == null)
                //    return new BaseResponse<string> { Status = Status.ERROR, Message = "User and password don't match" };
                //else if (token.Value == Guid.Empty)
                //    return new BaseResponse<string> { Status = Status.ERROR, Message = "User and password don't match" };

                //if (_dao.Insert(token))
                //    return new BaseResponse<string> { Status = Status.OK, Content = token.Value.ToString() , Message = "Success"};
                //else
                //    return new BaseResponse<string> { Status = Status.ERROR, Message = "Unexpected Error" };
            }
            catch (Exception)
            {
                return new BaseResponse<string> { Status = Status.ERROR, Message = "Unexpected Error" };
            }

        }

        public static ClaimsPrincipal GetPrincipal(string token)
        {
            try
            {
                JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();
                JwtSecurityToken jwtToken = tokenHandler.ReadToken(token) as JwtSecurityToken;

                if (jwtToken == null)
                {
                    return null;
                }

                byte[] symmetricKey = Convert.FromBase64String(SecretHandler.Secret);

                TokenValidationParameters validationParameters = new TokenValidationParameters()
                {
                    RequireExpirationTime = true,
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    IssuerSigningKey = new SymmetricSecurityKey(symmetricKey)
                };

                ClaimsPrincipal principal = tokenHandler.ValidateToken(token, validationParameters, out SecurityToken securityToken);

                return principal;
            }
            catch (Exception)
            {
                //should write log
                return null;
            }
        }

        protected Task<IPrincipal> AuthenticateJwtToken(string token)
        {

            if (ValidToken(token, out string username))
            {
                // based on username to get more information from database 
                // in order to build local identity
                List<Claim> claims = new List<Claim>
        {
            new Claim(ClaimTypes.Name, username)
            // Add more claims if needed: Roles, ...
        };

                ClaimsIdentity identity = new ClaimsIdentity(claims, "Jwt");
                IPrincipal user = new ClaimsPrincipal(identity);

                return Task.FromResult(user);
            }

            return Task.FromResult<IPrincipal>(null);
        }
        public bool ValidToken(string token, out string username)
        {
            username = null;
            if (token == null)
            {
                return false;
            }

            try
            {
                ClaimsPrincipal simplePrinciple = GetPrincipal(token);
                ClaimsIdentity identity = simplePrinciple.Identity as ClaimsIdentity;

                if (identity == null)
                {
                    return false;
                }

                if (!identity.IsAuthenticated)
                {
                    return false;
                }

                Claim usernameClaim = identity.FindFirst(ClaimTypes.Name);
                username = usernameClaim?.Value;

                if (string.IsNullOrEmpty(username))
                {
                    return false;
                }

                // More validate to check whether username exists in system

                return true;
                //return _dao.ValidateToken(token);
            }
            catch (Exception)
            {
                return false;
            }


        }
    }
}