using Backend_Web.DAL.DAO_s;
using Backend_Web.Models;
using Backend_Web.Services;
using Backend_Web.Utils;
using System.Web.Http;

namespace Backend_Web.Controllers
{
    public class UserController : CRUDController<User, UserService, UserDAO>
    {
    }
}
