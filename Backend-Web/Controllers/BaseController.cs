using Backend_Web.Utils;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;

namespace Backend_Web.Controllers
{
    [EnableCors("*", headers: "*", methods: "*")]
    [TokenAuthorization]
    public abstract class BaseController<TService> : ApiController where TService : class, new()
    {
        public BaseController()
        {
            _service = new TService();
        }

        protected TService _service;


    }
}
