using Backend_Web.DAL.DAO_s;
using Backend_Web.Models;
using Backend_Web.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.Http;

namespace Backend_Web.Services
{
    public class AuthService : BaseService<Token, TokenDAO>
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

                Token token = this._dao.Login(username, password);

                if (token == null)
                    return new BaseResponse<string> { Status = Status.ERROR, Message = "User not found" };
                else if (token.Value == Guid.Empty)
                    return new BaseResponse<string> { Status = Status.ERROR, Message = "User and password don't match" };

                if (_dao.Insert(token))
                    return new BaseResponse<string> { Status = Status.OK, Content = token.Value.ToString() , Message = "Success"};
                else
                    return new BaseResponse<string> { Status = Status.ERROR, Message = "Unexpected Error" };
            }
            catch (Exception ex)
            {
                return new BaseResponse<string> { Status = Status.ERROR, Message = "Unexpected Error" };
            }
            
        }

        public bool ValidToken(string token)
        {
            return _dao.ValidateToken(token);
        }
    }
}