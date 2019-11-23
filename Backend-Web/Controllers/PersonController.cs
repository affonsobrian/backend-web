using Backend_Web.DAL.DAO_s;
using Backend_Web.Models;
using Backend_Web.Services;
using Backend_Web.Utils;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Net.Http;
using System.Text;
using System.Text.RegularExpressions;
using System.Web.Http;

namespace Backend_Web.Controllers
{
    public class PersonController : CRUDController<Person, PersonService, PersonDAO>
    {
        #region .: Overridden Methods :.

        protected override BaseResponse<bool> VatidateObject(Person element)
        {
            try
            {
                if (string.IsNullOrEmpty(element.Name))
                {
                    return new BaseResponse<bool> { Content = false, Message = Resources.ErrorMessages.missingName };
                }

                if (string.IsNullOrEmpty(element.Email) || !new EmailAddressAttribute().IsValid(element.Email))
                {
                    return new BaseResponse<bool> { Content = false, Message = Resources.ErrorMessages.invalidEmail };
                }

                if (string.IsNullOrEmpty(element.Telephone) || !ValidateTelephone(element.Telephone))
                {
                    return new BaseResponse<bool> { Content = false, Message = Resources.ErrorMessages.invalidTelephone };
                }

                if (string.IsNullOrEmpty(element.RG))
                {
                    return new BaseResponse<bool> { Content = false, Message = Resources.ErrorMessages.emptyRG };
                }

                return base.VatidateObject(element);
            }
            catch (Exception)
            {
                return new BaseResponse<bool> { Status = Status.ERROR, Message = Resources.ErrorMessages.unexpectedError };
            }
        }

        #endregion

        #region .: Public Methods :.

        //[HttpGet]
        //[Route("api/person/properties/{id}")]
        //public BaseResponse<List<Property>> Properties(int id)
        //{
        //    try
        //    {
        //        return new BaseResponse<List<Property>> { Status = Status.OK, Message = Resources.Commun.success, Content = new List<Property>() };
        //    }
        //    catch(Exception)
        //    {
        //        return new BaseResponse<List<Property>> { Status = Status.ERROR, Message = Resources.ErrorMessages.unexpectedError };
        //    }
        //}

        #endregion

        #region .: Private Methods :.

        private bool ValidateTelephone(string telephone)
        {
            try
            {
                Regex regex = new Regex("^\\([1-9]{2}\\) [9]{0,1}[1-9]{1}[0-9]{3}\\-[0-9]{4}$");
                return regex.IsMatch(telephone);
            }
            catch (Exception)
            {
                return false;
            }
        }

        #endregion
    }
}