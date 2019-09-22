using Backend_Web.StaticClasses;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Dynamic;

namespace Backend_Web.DAL.DAO_s
{
    /// <summary>
    /// Abstract class for the data access layer
    /// </summary>
    /// <typeparam name="TModel">The model of database</typeparam>
    public class BaseDAO<TModel> where TModel : class
    {
        #region .: Constructors :.

        public BaseDAO()
        {
            this.db = DatabaseHandler.Database.GetTable<TModel>();
        }

        #endregion

        #region .: Properties :.

        protected readonly DbSet<TModel> db;

        #endregion

        #region .: Public Methods :.

        public bool Insert(TModel element)
        {
            db.Add(element);
            return DatabaseHandler.Database.SaveChanges() > 0;
        }

        public TModel FindById(int id)
        {
            return this.db.Find(id);
        }

        public List<TModel> Query(string parameter, string value)
        {
            IQueryable<TModel> dbSet = this.db;
            IQueryable<TModel> models = dbSet.Where(parameter + " = @0", value);
            return models.ToList();
        }

        #endregion
    }
}