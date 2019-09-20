using Backend_Web.Utils;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Backend_Web.Models.DAL
{
    public class DatabaseContext : DbContext
    {
        #region .: Constructors :.
        public DatabaseContext() : base("DatabaseConnectionString") {
            tables = new TypeDictionary<object>
            {
                { typeof(User), Users }
            };
        }

        #endregion

        #region .: Properties :.
        public DbSet<User> Users { get; set; }

        private TypeDictionary<object> tables;

        public DbSet<T> GetTable<T>() where T : class
        {
            return (DbSet<T>) this.tables.Get<T>();
        }
        #endregion
    }

}