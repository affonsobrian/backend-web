using Backend_Web.DAL.DAO_s;
using Backend_Web.Models;
using Backend_Web.Services;
using Backend_Web.Utils;
using System;
using System.ComponentModel.DataAnnotations;
using System.Net.Http;
using System.Text;
using System.Text.RegularExpressions;

namespace Backend_Web.Controllers
{
    public class PersonController : BaseController<Person, PersonService, PersonDAO>
    {
        protected override BaseResponse<bool> VatidateObject(Person element)
        {
            if (element.Id != 0)
            {
                return new BaseResponse<bool> { Content = false, Message = "Element id needs to be 0" };
            }

            if (string.IsNullOrEmpty(element.FirstName))
            {
                return new BaseResponse<bool> { Content = false, Message = "First name can't be empty" };
            }

            if (string.IsNullOrEmpty(element.LastName))
            {
                return new BaseResponse<bool> { Content = false, Message = "Last name can't be empty" };
            }

            if (string.IsNullOrEmpty(element.Email) || !new EmailAddressAttribute().IsValid(element.Email))
            {
                return new BaseResponse<bool> { Content = false, Message = "Invalid e-mail" };
            }

            if (string.IsNullOrEmpty(element.Telephone) || !ValidateTelephone(element.Telephone))
            {
                return new BaseResponse<bool> { Content = false, Message = "Invalid telephone" };
            }

            if (string.IsNullOrEmpty(element.RG) || !ValidateRG(element.RG))
            {
                return new BaseResponse<bool> { Content = false, Message = "Invalid RG" };
            }

            return base.VatidateObject(element);
        }

        private bool ValidateTelephone(string telephone)
        {
            try
            {
                Regex regex = new Regex("^\\([1-9]{2}\\) [9]{0,1}[1-9]{1}[0-9]{3}\\-[0-9]{4}$");
                return regex.IsMatch(telephone);
            }
            catch(Exception ex)
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
    }
}