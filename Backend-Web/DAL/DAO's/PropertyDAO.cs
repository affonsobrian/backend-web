using Backend_Web.Models;
using System.Collections.Generic;
using System.Linq;

namespace Backend_Web.DAL.DAO_s
{
    public class PropertyDAO : BaseDAO<Property>
    {
        public virtual List<Property> List(int personId)
        {
            return db.Where(_ => _.Person.Id == personId).ToList();
        }
    }
}