using Backend_Web.Models;
using Backend_Web.Models.DAL;
using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Web;

namespace Backend_Web.DAL
{
    internal sealed class Configuration : DbMigrationsConfiguration<DatabaseContext>
    {
        public Configuration() => this.AutomaticMigrationsEnabled = true;

        public void RunSeed(DatabaseContext context) => Seed(context);

        protected override void Seed(DatabaseContext context)
        {
            if (context.Users.Count() == 0)
                context.Users.Add(new User { Name = "admin", Password = "admin123", Email = "admin@admin.com" });

            context.SaveChanges();
        }
    }
}