using Backend_Web.Utils;
using System.Data.Entity;

namespace Backend_Web.Models.DAL
{
    public class DatabaseContext : DbContext
    {
        #region .: Constructors :.
        public DatabaseContext() : base("DatabaseConnectionString")
        {
            Tables = new TypeDictionary<object>
            {
                { typeof(User), Users },
                { typeof(Property), Properties },
                { typeof(Person), People },
                { typeof(Transaction), Transactions },
                { typeof(History), Histories }
            };
        }

        #endregion

        #region .: Properties :.
        public DbSet<User> Users { get; set; }
        public DbSet<Property> Properties { get; set; }
        public DbSet<Person> People { get; set; }
        public DbSet<Transaction> Transactions { get; set; }
        public DbSet<History> Histories { get; set; }

        private readonly TypeDictionary<object> Tables;

        #endregion

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }

        public DbSet<T> GetTable<T>() where T : class
        {
            return (DbSet<T>)Tables.Get<T>();
        }
    }

}