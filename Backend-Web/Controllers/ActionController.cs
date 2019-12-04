using Backend_Web.Models.ApiBody;
using Backend_Web.Services;
using Backend_Web.Utils;
using System;
using System.Web.Http;

namespace Backend_Web.Controllers
{
    [RoutePrefix("api/action")]
    public class ActionController : BaseController<ActionService>
    {
        [HttpPost]
        [Route("borrow")]
        public BaseResponse<bool> Borrow([FromBody] Borrow borrow)
        {
            if (!DateTime.TryParse(borrow.Date, out DateTime date))
            {
                return new BaseResponse<bool> { Status = Status.ERROR, Message = Resources.ErrorMessages.parseDateError, Content = false };
            }

            if (borrow.PersonId == 0)
            {
                if(string.IsNullOrEmpty(borrow.Email))
                {
                    return new BaseResponse<bool> { Status = Status.ERROR, Message = Resources.ErrorMessages.oneRequired, Content = false };
                }
                else
                {
                    return _service.Borrow(borrow.Email, borrow.Properties, date, borrow.Image);
                }
            }
            else
            {
                return _service.Borrow(borrow.PersonId, borrow.Properties, date, borrow.Image);
            }
            
        }


        [HttpPost]
        [Route("return")]
        public BaseResponse<bool> Return([FromBody] Return returnItem)
        {
            if (!DateTime.TryParse(returnItem.Date, out DateTime date))
            {
                return new BaseResponse<bool> { Status = Status.ERROR, Message = Resources.ErrorMessages.parseDateError, Content = false };
            }

            if (returnItem.Person == 0)
            {
                if (string.IsNullOrEmpty(returnItem.Email))
                {
                    return new BaseResponse<bool> { Status = Status.ERROR, Message = Resources.ErrorMessages.oneRequired, Content = false };
                }
                else
                {
                    return _service.Return(returnItem.Email, returnItem.Properties, date);
                }
            }
            else
            {
                return _service.Return(returnItem.Person, returnItem.Properties, date);
            }
        }
    }
}
