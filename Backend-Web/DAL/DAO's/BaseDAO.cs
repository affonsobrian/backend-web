using Backend_Web.Models;
using Backend_Web.StaticClasses;
using Backend_Web.Utils;
using System;
using System.Data.Entity;

namespace Backend_Web.DAL.DAO_s
{
    public class BaseDAO<T> where T : class
    {
        #region .: Constructors :.

        public BaseDAO()
        {
            this.db = DatabaseHandler.Database.GetTable<T>();
            this.logger = LoggerManager.GetDefaultLogger(typeof(T).GetType().Name);
        }

        #endregion

        #region .: Properties :.

        private readonly DbSet<T> db;

        private readonly Logger logger;

        #endregion

        #region .: Public Methods :.

        public bool Insert(T element)
        {
            try
            {
                this.db.Add(element);
                DatabaseHandler.Database.SaveChanges();
                return true;
            }
            catch(Exception ex)
            {
                logger.Error(ex, "Error while inserting element");
                return false;
            }

        }

        public T FindById(int id)
        {
            try
            {
                return this.db.Find(id);
            }
            catch(Exception ex)
            {
                logger.Error(ex, "Error finding an element");
                return null;
            }
        }

        #endregion
    }
}