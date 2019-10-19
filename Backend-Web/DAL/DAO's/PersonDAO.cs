using Backend_Web.Models;

namespace Backend_Web.DAL.DAO_s
{
    public class PersonDAO : BaseDAO<Person>
    {
        public override bool Remove(int id)
        {
            Person element = FindById(id);
            element.Active = false;
            return Edit(element);
        }
    }
}