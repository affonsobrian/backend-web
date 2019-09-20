using Backend_Web.DAL.DAO_s;
using Backend_Web.Models;
using Backend_Web.Models.DAL;
using System.Web.Http;

namespace Backend_Web.Controllers
{
    public class UserController : ApiController
    {
        public User Get()
        {
            UserDAO userDAO = new UserDAO();
            User user = new User { Name = "Affs", Email = "Affs@aff.com", Password = "Comida" };
            userDAO.Insert(user);
            return user;
        }
    }
}
