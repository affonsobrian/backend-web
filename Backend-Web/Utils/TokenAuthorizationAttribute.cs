using Backend_Web.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using System.Web.Http.Controllers;

namespace Backend_Web.Utils
{
    public class TokenAuthorizationAttribute : AuthorizeAttribute
    {
        protected override bool IsAuthorized(HttpActionContext actionContext)
        {
            //if (!actionContext.Request.RequestUri.AbsolutePath.Contains("Auth") && actionContext.Request.Headers.Referrer.AbsolutePath != "/swagger/ui/index" && actionContext.Request.Headers.Authorization?.Scheme != "Bearer" && new AuthService().ValidToken(actionContext.Request.Headers.Authorization.Parameter))
            //{
            //    return false;
            //}
            return true;
        }
    }
}