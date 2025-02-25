using Ecom.API.Errors;
using Ecom.Infrastructure.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Ecom.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BugController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public BugController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet("Not-Found")]

        public ActionResult GetNotFound()
        {
            var product = _context.Products.Find(50);
            if (product == null)
            {
                return NotFound(new BaseCommonResponse(404));
            }
            return Ok(product);
        }

        [HttpGet("Server-Error")]

        public ActionResult GetServerError()
        {
            var product = _context.Products.Find(50);
            product.name = "Error";
            return Ok();
        }

        [HttpGet("Bad-Request/{id}")]

        public ActionResult GetNotFoundRequest(int id)
        {
            return Ok();
        }

        [HttpGet("Bad-Request")]

        public ActionResult GetBadRequest()
        {
            return BadRequest(new BaseCommonResponse(400));
        }
    }
}
