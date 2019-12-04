using Backend_Web.DAL.DAO_s;
using Backend_Web.Models;
using Backend_Web.Utils;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Backend_Web.Services
{
    public class PersonService : CRUDService<Person, PersonDAO>
    {
        public override BaseResponse<Person> Find(int id)
        {
            BaseResponse<Person> response = base.Find(id);
            if (response.Status == Status.OK && (response.Content?.Active ?? false))
            {
                return response;
            }

            return new BaseResponse<Person> { Status = Status.OK, Content = null, Message = Resources.ErrorMessages.notFound };
        }

        public override BaseResponse<string> Edit(Person element)
        {
            if (element.Id == 0 && CheckEmail(element?.Email ?? string.Empty).Content)
            {
                element.Id = FindByEmail(element.Email).Content.Id;
            }
            return base.Edit(element);
        }

        public BaseResponse<bool> CheckEmail(string email)
        {
            BaseResponse<Person> personResponse = FindByEmail(email);
            BaseResponse<bool> response = new BaseResponse<bool>();
            if(personResponse.Status == Status.OK)
            {
                if(personResponse.Content.Active)
                {
                    response.Status = Status.OK;
                    response.Message = Resources.Commun.success;
                    response.Content = true;
                }
                else
                {
                    response.Status = Status.ERROR;
                    response.Message = Resources.ErrorMessages.inactiveEmail;
                    response.Content = true;
                }
            }
            else
            {
                response.Status = personResponse.Status;
                response.Message = personResponse.Message;
                response.Content = false;
            }

            return response;
        }

        public BaseResponse<Person> FindByEmail(string email)
        {
            Person response = _dao.Query("Email", email).FirstOrDefault();
            if (response != null)
            {
                return new BaseResponse<Person> { Status = Status.OK, Message = Resources.Commun.success, Content = response };
            }

            return new BaseResponse<Person> { Status = Status.NOTFOUND, Content = null, Message = Resources.ErrorMessages.notFound };
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
    }
}