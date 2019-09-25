using Backend_Web.DAL.DAO_s;
using Backend_Web.Services;
using Backend_Web.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Http.Cors;

namespace Backend_Web.Controllers
{
    [EnableCors("http://localhost:3000", headers: "*", methods: "*")]
    [TokenAuthorization]
    public abstract class BaseController<TModel, TService, TDao> : ApiController where TModel : class where TService : BaseService<TModel, TDao>, new() where TDao : BaseDAO<TModel>, new()
    {

        #region .: Contructors :.
        public BaseController()
        {
            this._service = new TService();
        }

        #endregion

        #region .: Properties :.

        public TService _service { get; set; }

        #endregion

        #region .: Public Methods :.

        [HttpGet]
        public virtual BaseResponse<TModel> Get(int id)
        {
            return _service.Find(id);
        }

        [HttpPost]
        public virtual BaseResponse<string> Post(TModel element)
        {
            BaseResponse<bool> response = this.VatidateObject(element);
            if (response.Content)
                return _service.Insert(element);
            return new BaseResponse<string> { Status = Status.ERROR, Message = response.Message };
        }

        [HttpPut]
        public virtual BaseResponse<string> Put(TModel element)
        {
            return _service.Edit(element);
        }

        [HttpDelete]
        public virtual BaseResponse<string> Delete(int id)
        {
            return _service.Remove(id);
        }

        [HttpGet]
        [Route("api/{controller}/")]
        [EnableCors("http://localhost:3000", headers: "*", methods: "*")]
        public virtual BaseResponse<List<TModel>> List()
        {
            return _service.List();
        }

        protected virtual BaseResponse<bool> VatidateObject(TModel element)
        {
            return new BaseResponse<bool> { Content = true };
        }

        #endregion
    }
}