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
            Tables = new TypeDictionary<object>
            {
                { typeof(User), Users },
                { typeof(Property), Properties },
                { typeof(Token), Token },
                { typeof(Person), People }
            };
        }

        #endregion

        #region .: Properties :.
        public DbSet<User> Users { get; set; }
        public DbSet<Property> Properties { get; set; }
        public DbSet<Token> Token { get; set; }
        public DbSet<Person> People { get; set; }

        private TypeDictionary<object> Tables;

        #endregion

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }

        public DbSet<T> GetTable<T>() where T : class
        {
            return (DbSet<T>) this.Tables.Get<T>();
        }
    }

}