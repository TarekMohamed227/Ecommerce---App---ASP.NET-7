
using Ecom.API.Errors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;


namespace Ecom.API.Controllers
{
    [Route("errors/{StatusCode}")]
    [ApiController]


    public class ErrorController : ControllerBase
    {
        [HttpGet]
        public IActionResult Error(int StatusCode)
        {
            return new ObjectResult(new BaseCommonResponse(StatusCode));
        }
    }
}
