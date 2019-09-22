using Backend_Web.Models;
using Backend_Web.Models.DAL;
using System.Data.Entity.Migrations;
using System.Linq;  

namespace Backend_Web.DAL
{
    internal sealed class Configuration : DbMigrationsConfiguration<DatabaseContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
        }

        public void RunSeed(DatabaseContext context)
        {
            Seed(context);
        }

        protected override void Seed(DatabaseContext context)
        {
            if (context.Users.Count() == 0)
            {
                context.Users.Add(new User { Name = "admin", Password = "admin123" });
            }

            context.SaveChanges();
        }
    }
}