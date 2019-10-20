using Backend_Web.DAL.DAO_s;
using Backend_Web.Models;
using Backend_Web.Utils;
using System;
using System.Collections.Generic;

namespace Backend_Web.Services
{
    public class PersonService : BaseService<Person, PersonDAO>
    {
        public override BaseResponse<Person> Find(int id)
        {
            BaseResponse<Person> response = base.Find(id);
            if (response.Status == Status.OK && (response.Content?.Active ?? false))
            {
                return response;
            }

            return new BaseResponse<Person> { Status = Status.OK, Content = null, Message = "Object not found" };
        }

        public override BaseResponse<List<Person>> List()
        {
            BaseResponse<List<Person>> response = base.List();
            if (response.Status == Status.OK && response.Content.Count > 0)
            {
                response.Content = response.Content.FindAll(_ => _.Active);
            }

            return response;
        }

        public BaseResponse<string> Borrow(int id, int property)
        {
            return new BaseResponse<string> { Status = Status.OK, Message = Resources.Commun.success };
        }
    }
}