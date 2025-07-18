using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AccessDesk_ASP_Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BaseControllerController : ControllerBase
    {
        protected string GetUserId()
        {
            return User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value ?? string.Empty;
        }
    }
}
