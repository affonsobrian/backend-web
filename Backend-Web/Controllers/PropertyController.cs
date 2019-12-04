using Backend_Web.DAL.DAO_s;
using Backend_Web.Models;
using Backend_Web.Services;
using Backend_Web.Utils;
using System.Collections.Generic;
using System.Web.Http;

namespace Backend_Web.Controllers
{
    [RoutePrefix("api/property")]
    public class PropertyController : CRUDController<Property, PropertyService, PropertyDAO>
    {
        public override BaseResponse<string> Post(Property element)
        {
            element.Status = "Disponível";
            return base.Post(element);
        }


        [Route("list/{personId}")]
        [HttpGet]
        public BaseResponse<List<Property>> ListPerPerson(int personId)
        {
            return _service.List(personId);
            //return new BaseResponse<List<string>> { Content = new List<string> { "sdfsdf", "sdfsdf" } };
        }
    }
}