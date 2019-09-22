using Backend_Web.DAL.DAO_s;
using Backend_Web.Models;
using Backend_Web.Utils;
using System.Security.Cryptography;
using System.Text;

namespace Backend_Web.Services
{
    /// <summary>
    /// The service layer of the <seealso cref="User"/> model
    /// </summary>
    public class UserService : BaseService<User, UserDAO>
    {
        public override BaseResponse<string> Insert(User element)
        {
            using (SHA256 mySHA256 = SHA256.Create())
            {
                byte [] PasswordHash = mySHA256.ComputeHash(Encoding.ASCII.GetBytes(element.Password));
                element.Password = Encoding.ASCII.GetString(PasswordHash);
            }
            return base.Insert(element);
        }
    }
}