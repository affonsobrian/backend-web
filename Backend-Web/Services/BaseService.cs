using Backend_Web.DAL.DAO_s;
using Backend_Web.Utils;
using System;
using System.Collections.Generic;

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
            _dao = new TDao();
            logger = LoggerManager.GetDefaultLogger(typeof(TModel).Name);
        }

        #endregion


        #region .: Properties :.

        public readonly TDao _dao;
        private readonly Logger logger;

        #endregion

        #region .: Public Methods :.

        public virtual BaseResponse<TModel> Find(int id)
        {
            try
            {
                TModel element = _dao.FindById(id);
                return new BaseResponse<TModel> { Status = Status.OK, Content = element };
            }
            catch (Exception ex)
            {
                logger.Error(ex, "Error getting " + typeof(TModel).Name);
                return new BaseResponse<TModel> { Status = Status.ERROR, Message = "Error finding object" };
            }
        }

        public virtual BaseResponse<string> Insert(TModel element)
        {
            try
            {
                if (_dao.Insert(element))
                {
                    return new BaseResponse<string> { Status = Status.OK, Message = "Success" };
                }
                else
                {
                    return new BaseResponse<string> { Status = Status.ERROR, Message = "Failed" };
                }
            }
            catch (Exception ex)
            {
                logger.Error(ex, "Error inserting " + typeof(TModel).Name);
                return new BaseResponse<string> { Status = Status.ERROR, Message = "Failed" };
            }
        }

        public virtual BaseResponse<string> Edit(TModel element)
        {
            try
            {
                if (_dao.Edit(element))
                {
                    return new BaseResponse<string> { Status = Status.OK, Message = "Success" };
                }
                else
                {
                    return new BaseResponse<string> { Status = Status.ERROR, Message = "Failed" };
                }
            }
            catch (Exception ex)
            {
                logger.Error(ex, "Error editing " + typeof(TModel).Name);
                return new BaseResponse<string> { Status = Status.ERROR, Message = "Failed" };
            }
        }

        public virtual BaseResponse<string> Remove(int id)
        {
            try
            {
                if (_dao.Remove(id))
                {
                    return new BaseResponse<string> { Status = Status.OK, Message = "Success" };
                }
                else
                {
                    return new BaseResponse<string> { Status = Status.ERROR, Message = "Failed" };
                }
            }
            catch (Exception ex)
            {
                logger.Error(ex, "Error removing " + typeof(TModel).Name);
                return new BaseResponse<string> { Status = Status.ERROR, Message = "Failed" };
            }
        }
        public virtual BaseResponse<List<TModel>> List()
        {
            try
            {
                List<TModel> element = _dao.List();
                return new BaseResponse<List<TModel>> { Status = Status.OK, Content = element, Message = "Success" };
            }
            catch (Exception ex)
            {
                logger.Error(ex, "Error getting " + typeof(TModel).Name);
                return new BaseResponse<List<TModel>> { Status = Status.ERROR, Message = ex.Message, Content = null };
            }
        }

        #endregion
    }
}