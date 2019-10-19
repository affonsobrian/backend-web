using Backend_Web.Models;
using Backend_Web.Models.DAL;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

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
                using (SHA256 mySHA256 = SHA256.Create())
                {
                    byte[] PasswordHash = mySHA256.ComputeHash(Encoding.ASCII.GetBytes("admin123"));
                    context.Users.Add(new User { Name = "admin", Password = Encoding.ASCII.GetString(PasswordHash) });
                }
            }

            context.SaveChanges();
        }
    }
}