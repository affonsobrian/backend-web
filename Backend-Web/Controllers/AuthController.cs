using Backend_Web.DAL.DAO_s;
using Backend_Web.Models;
using Backend_Web.Services;
using Backend_Web.Utils;
using System.Collections.Generic;
using System.Web.Http;

namespace Backend_Web.Controllers
{
    public class AuthController : BaseController<Token, AuthService, TokenDAO>
    {
        [HttpPost]
        public BaseResponse<string> Login([FromBody] Dictionary<string, string> data)
        {
            Token token = _service.Login(data["username"], data["password"]).Content;
            return new BaseResponse<string>() { Content = token.Value.ToString(), Status = Status.OK };
        }
    }

}