using Backend_Web.Models;
using System.Linq;

namespace Backend_Web.DAL.DAO_s
{
    public class UserDAO : BaseDAO<User>
    {
        public UserDAO() : base()
        {
        }

        public User FindByName(string name)
        {
            User user = Query("NAME", name).FirstOrDefault();
            return user;
        }
    }
}