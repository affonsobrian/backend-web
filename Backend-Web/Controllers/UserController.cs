using Backend_Web.DAL.DAO_s;
using Backend_Web.Models;
using Backend_Web.Services;
using Backend_Web.Utils;
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
            UserService userService = new UserService();
            BaseResponse<User> response = userService.Get(id);
            if (response.Status == Status.OK)
                return response.Content;
            return null;
        }

        public string Post([FromBody] User user)
        {
            UserService userService = new UserService();
            BaseResponse<string> response = userService.Insert(user);
            return response.Content;
        }
    }
}
