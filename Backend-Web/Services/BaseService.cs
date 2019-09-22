using Backend_Web.DAL.DAO_s;
using Backend_Web.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Backend_Web.Services
{
    /// <summary>
    /// Abstract class for the service layer
    /// </summary>
    /// <typeparam name="TModel">The data model</typeparam>
    /// <typeparam name="TDao">The data access object</typeparam>
    public abstract class BaseService<TModel, TDao> where TModel : class where TDao : BaseDAO<TModel>, new()
    {
        #region .: Constructor :.

        public BaseService()
        {
            this._dao = new TDao();
            this.logger = LoggerManager.GetDefaultLogger(typeof(TModel).Name);
        }

        #endregion


        #region .: Properties :.

        public readonly TDao _dao;
        private readonly Logger logger;

        #endregion

        #region .: Public Methods :.

        public virtual BaseResponse<TModel> Get(int id)
        {
            try
            {
                TModel element = _dao.FindById(id);
                return new BaseResponse<TModel> { Status = Status.OK, Content = element};
            }catch(Exception ex)
            {
                logger.Error(ex, "Error getting " + typeof(TModel).Name);
                return new BaseResponse<TModel> { Status = Status.ERROR };
            }
        }

        public virtual BaseResponse<string> Insert(TModel element)
        {
            try
            {
                if (this._dao.Insert(element))
                    return new BaseResponse<string> { Status = Status.OK, Content = "Success" };
                else
                    return new BaseResponse<string> { Status = Status.ERROR, Content = "Failed" };
            }
            catch(Exception ex)
            {
                logger.Error(ex, "Error inserting " + typeof(TModel).Name);
                return new BaseResponse<string> { Status = Status.ERROR, Content = "Failed" };
            }
        }

        #endregion
    }
}