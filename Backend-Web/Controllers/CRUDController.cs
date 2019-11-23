using Backend_Web.DAL.DAO_s;
using Backend_Web.Services;
using Backend_Web.Utils;
using System;
using System.Collections.Generic;
using System.Web.Http;
using System.Web.Http.Cors;

namespace Backend_Web.Controllers
{
    public abstract class CRUDController<TModel, TService, TDao> : BaseController<TService> where TModel : class where TService : CRUDService<TModel, TDao>, new() where TDao : BaseDAO<TModel>, new()
    {

        #region .: Contructors :.
        public CRUDController()
        {
            _service = new TService();
        }

        #endregion

        #region .: Public Methods :.

        [HttpGet]
        public virtual BaseResponse<TModel> Get(int id)
        {
            try
            {
                return _service.Find(id);
            }
            catch (Exception)
            {
                return new BaseResponse<TModel> { Status = Status.ERROR, Message = Resources.ErrorMessages.unexpectedError };
            }
        }

        [HttpPost]
        public virtual BaseResponse<string> Post(TModel element)
        {
            try
            {
                BaseResponse<bool> response = VatidateObject(element);
                if (response.Content)
                {
                    if ((int) element.GetType().GetProperty("Id").GetValue(element) == 0)
                    {
                        return _service.Insert(element);
                    }
                    else
                    {
                        return _service.Edit(element);
                    }
                }

                return new BaseResponse<string> { Status = Status.ERROR, Message = response.Message };
            }
            catch (Exception)
            {
                return new BaseResponse<string> { Status = Status.ERROR, Message = Resources.ErrorMessages.unexpectedError };
            }
        }

        [HttpPut]
        public virtual BaseResponse<string> Put(TModel element)
        {
            try
            {
                BaseResponse<bool> response = VatidateObject(element);
                if (response.Content)
                {
                    return _service.Edit(element);
                }

                return new BaseResponse<string> { Status = Status.ERROR, Message = response.Message };
            }
            catch (Exception)
            {
                return new BaseResponse<string> { Status = Status.ERROR, Message = Resources.ErrorMessages.unexpectedError };
            }
        }

        [HttpDelete]
        public virtual BaseResponse<string> Delete(int id)
        {
            try
            {
                return _service.Remove(id);
            }
            catch (Exception)
            {
                return new BaseResponse<string> { Status = Status.ERROR, Message = Resources.ErrorMessages.unexpectedError };
            }
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
            try
            {
                return new BaseResponse<bool> { Content = true };
            }
            catch (Exception)
            {
                return new BaseResponse<bool> { Status = Status.ERROR, Message = Resources.ErrorMessages.unexpectedError };
            }
        }

        #endregion
    }
}