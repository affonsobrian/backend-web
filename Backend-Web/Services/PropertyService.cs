using Backend_Web.DAL.DAO_s;
using Backend_Web.Models;
using Backend_Web.Utils;
using System;
using System.Collections.Generic;

namespace Backend_Web.Services
{
    public class PropertyService : CRUDService<Property, PropertyDAO>
    {
        public virtual BaseResponse<List<Property>> List(int personId)
        {
            try
            {
                List<Property> element = _dao.List(personId);
                return new BaseResponse<List<Property>> { Status = Status.OK, Content = element, Message = Resources.Commun.success };
            }
            catch (Exception ex)
            {
                logger.Error(ex, "Error getting " + typeof(Property).Name);
                return new BaseResponse<List<Property>> { Status = Status.ERROR, Message = Resources.ErrorMessages.unexpectedError, Content = null };
            }
        }

        public override BaseResponse<List<Property>> List()
        {
            BaseResponse<List<Property>> response = base.List();
            if (response.Status == Status.OK && response.Content.Count > 0)
            {
                response.Content = response.Content.FindAll(_ => _.Active);
            }

            return response;
        }
    }
}