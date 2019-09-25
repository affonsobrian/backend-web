using Backend_Web.DAL.DAO_s;
using Backend_Web.Models;
using Backend_Web.Services;
using Backend_Web.Utils;
using System.Collections.Generic;
using System.Web.Http;
using System.Web.Http.Cors;

namespace Backend_Web.Controllers
{
    public class AuthController : BaseController<Token, AuthService, TokenDAO>
    {
        [HttpPost]
        public BaseResponse<string> Login([FromBody] Dictionary<string, string> data)
        {
            return _service.Login(data["username"], data["password"]);
        }

        [NonAction]
        public override BaseResponse<Token> Get(int id)
        {
            return null;
        }

        [NonAction]
        public override BaseResponse<string> Post(Token element)
        {
            return null;
        }

        [NonAction]
        public override BaseResponse<string> Put(Token element)
        {
            return null;
        }

        [NonAction]
        public override BaseResponse<string> Delete(int id)
        {
            return null;
        }

        [NonAction]
        public override BaseResponse<List<Token>> List()
        {
            return null;
        }
    }

}