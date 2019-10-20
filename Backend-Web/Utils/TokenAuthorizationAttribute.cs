using Backend_Web.Services;
using System.Web.Http;
using System.Web.Http.Controllers;

namespace Backend_Web.Utils
{
    public class TokenAuthorizationAttribute : AuthorizeAttribute
    {
        protected override bool IsAuthorized(HttpActionContext actionContext)
        {
#if !NoAuthentication

            if (actionContext.Request.Headers.Authorization?.Scheme == "Bearer" && new AuthService().ValidToken(actionContext.Request.Headers.Authorization?.Parameter, out string username))
            {
                return true;
            }
            else
            {
                return false;
            }
#else
            return true;
#endif
        }
    }
}