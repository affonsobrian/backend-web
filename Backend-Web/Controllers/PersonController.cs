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
                    return new BaseResponse<bool> { Content = false, Message = "First name can't be empty" };
                }

                if (string.IsNullOrEmpty(element.Email) || !new EmailAddressAttribute().IsValid(element.Email))
                {
                    return new BaseResponse<bool> { Content = false, Message = "Invalid e-mail" };
                }

                if (string.IsNullOrEmpty(element.Telephone) || !ValidateTelephone(element.Telephone))
                {
                    return new BaseResponse<bool> { Content = false, Message = "Invalid telephone" };
                }

                if (string.IsNullOrEmpty(element.RG))
                {
                    return new BaseResponse<bool> { Content = false, Message = "Invalid RG" };
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

        [HttpGet]
        [Route("api/person/properties/{id}")]
        public BaseResponse<List<Property>> Properties(int id)
        {
            try
            {
                return new BaseResponse<List<Property>> { Status = Status.OK, Message = Resources.Commun.success, Content = new List<Property>() };
            }
            catch(Exception)
            {
                return new BaseResponse<List<Property>> { Status = Status.ERROR, Message = Resources.ErrorMessages.unexpectedError };
            }
        }

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

        private bool ValidateRG(string RG)
        {
            try
            {
                using (HttpClient client = new HttpClient())
                {
                    client.BaseAddress = new Uri("https://www.4devs.com.br/ferramentas_online.php");
                    HttpResponseMessage response = client.PostAsync("", new StringContent("acao=validar_rg&txt_rg=" + RG, Encoding.UTF8, "application/x-www-form-urlencoded")).Result;
                    if (response.IsSuccessStatusCode)
                    {
                        string result = response.Content.ReadAsStringAsync().Result;
                        if (string.IsNullOrEmpty(result))
                        {
                            return false;
                        }

                        if (result.Split()[2] != "Verdadeiro")
                        {
                            return false;
                        }
                    }
                    else
                    {
                        return false;
                    }
                }
                return true;
            }
            catch (Exception)
            {
                return false;
            }

        } 

        #endregion
    }
}