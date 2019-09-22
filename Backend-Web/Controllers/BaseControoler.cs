using Backend_Web.DAL.DAO_s;
using Backend_Web.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;

namespace Backend_Web.Controllers
{
    public abstract class BaseController<TModel, TService, TDao> : ApiController where TModel : class where TService : BaseService<TModel, TDao>, new() where TDao : BaseDAO<TModel>, new()
    {
        public BaseController()
        {
            this._service = new TService();
        }
        public TService _service { get; set; }
    }
}