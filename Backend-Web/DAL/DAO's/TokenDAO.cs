using Backend_Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Backend_Web.DAL.DAO_s
{
    public class TokenDAO : BaseDAO<Token>
    {
        public Token Login(string username, string password)
        {
            UserDAO userDAO = new UserDAO();
            User user = userDAO.FindByName(username);

            if (user.Password == password)
                return new Token { User = user, Value = Guid.NewGuid(), LastRequest = DateTime.Now };
            else
                return new Token { User = user };
        }
    }
}