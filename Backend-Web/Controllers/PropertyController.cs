using Backend_Web.DAL.DAO_s;
using Backend_Web.Models;
using Backend_Web.Services;

namespace Backend_Web.Controllers
{
    public class PropertyController : CRUDController<Property, PropertyService, PropertyDAO>
    {
    }
}