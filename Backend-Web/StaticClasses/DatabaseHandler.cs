using Backend_Web.DAL;
using Backend_Web.Models.DAL;

namespace Backend_Web.StaticClasses
{
    public static class DatabaseHandler
    {
        public static readonly DatabaseContext Database = new DatabaseContext();

        internal static void Configure()
        {
            new Configuration().RunSeed(Database);
        }
    }
}