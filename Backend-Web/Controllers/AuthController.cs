using Backend_Web.Models;
using Backend_Web.Services;
using Backend_Web.Utils;
using System;
using System.Web.Http;

namespace Backend_Web.Controllers
{
    public class AuthController : BaseController<AuthService>
    {
        [HttpPost]
        [AllowAnonymous]
        public BaseResponse<string> Login(UserLogin user)
        {
            try
            {
                if (string.IsNullOrEmpty(user.username))
                {
                    return new BaseResponse<string> { Status = Status.ERROR, Message = Resources.ErrorMessages.missingUsername, Content = string.Empty };
                }
                if (string.IsNullOrEmpty(user.password))
                {
                    return new BaseResponse<string> { Status = Status.ERROR, Message = Resources.ErrorMessages.missingPassword, Content = string.Empty };
                }

                AuthService authService = new AuthService();
                return authService.Login(user.username, user.password);
            }
            catch (Exception ex)
            {
                return new BaseResponse<string> { Status = Status.ERROR, Message = ex.Message };
            }
        }
    }

}