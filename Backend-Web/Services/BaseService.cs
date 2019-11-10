using Backend_Web.Utils;

namespace Backend_Web.Services
{
    public abstract class BaseService<TModel>
    {

        #region .: Properties :.

        protected readonly Logger logger;

        #endregion

        #region .: Constructor :.

        public BaseService()
        {
            logger = LoggerManager.GetDefaultLogger(typeof(TModel).Name);
        }

        #endregion

    }
}