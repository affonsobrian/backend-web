using Backend_Web.StaticClasses;
using Backend_Web.Utils;
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
            db = DatabaseHandler.Database.GetTable<TModel>();
            logger = LoggerManager.GetDefaultLogger(typeof(TModel).Name);
        }

        #endregion

        #region .: Properties :.

        protected readonly DbSet<TModel> db;
        private readonly Logger logger;

        #endregion

        #region .: Public Methods :.

        public virtual TModel FindById(int id)
        {
            return db.Find(id);
        }

        public virtual bool Insert(TModel element)
        {
            db.Add(element);
            return DatabaseHandler.Database.SaveChanges() > 0;
        }

        public virtual bool Edit(TModel element)
        {
            DatabaseHandler.Database.Entry<TModel>(FindById((int)typeof(TModel).GetProperty("Id").GetValue(element))).CurrentValues.SetValues(element);
            return DatabaseHandler.Database.SaveChanges() > 0;
        }

        public virtual bool Remove(int id)
        {
            db.Remove(FindById(id));
            return DatabaseHandler.Database.SaveChanges() > 0;
        }

        public virtual List<TModel> List()
        {
            return db.Select(_ => _).ToList();
        }

        public List<TModel> Query(string parameter, string value)
        {
            try
            {
                IQueryable<TModel> dbSet = db;
                IQueryable<TModel> models = dbSet.Where(parameter + " = @0", value);
                return models.ToList();
            }
            catch (Exception ex)
            {
                logger.Warn(ex, "Query failed");
                return null;
            }
        }

        public List<TModel> Query(List<Tuple<string, string>> filters)
        {
            try
            {
                IQueryable<TModel> dbSet = db;
                string q = string.Empty;
                object[] values = new object[filters.Count];

                for (int i = 0; i < filters.Count; i++)
                {
                    q += string.Format("{0} = @{1}", filters[i].Item1, i);
                    values[i] = filters[i].Item2;
                }

                IQueryable<TModel> models = dbSet.Where(q, values);
                return models.ToList();
            }
            catch (Exception ex)
            {
                logger.Warn(ex, "Query failed");
                return null;
            }
        }

        #endregion
    }
}