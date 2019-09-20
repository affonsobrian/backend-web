using Backend_Web.Models;
using Backend_Web.StaticClasses;
using System.Data.Entity;

namespace Backend_Web.DAL.DAO_s
{
    public class BaseDAO<T> where T : class
    {
        #region .: Constructors :.

        public BaseDAO()
        {
            this.db = DatabaseHandler.Database.GetTable<T>();
        }

        #endregion

        #region .: Properties :.

        private readonly DbSet<T> db;

        #endregion

        #region .: Public Methods :.

        public void Insert(T element)
        {
            this.db.Add(element);
            DatabaseHandler.Database.SaveChanges();
        }

        #endregion
    }
}