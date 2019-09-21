using Backend_Web.DAL.DAO_s;
using Backend_Web.Models;
using System.Web.Http;

namespace Backend_Web.Controllers
{
    public class UserController : ApiController
    {
        /// <summary>
        /// Get the specified user
        /// </summary>
        ///  <param name="id">The id of the user</param>
        /// <returns></returns>
        public User Get(int id)
        {
            UserDAO userDAO = new UserDAO();
            User user = new User { Name = "Affs", Email = "Affs@aff.com", Password = "Comida" };
            userDAO.Insert(user);
            return user;
        }
    }
}
