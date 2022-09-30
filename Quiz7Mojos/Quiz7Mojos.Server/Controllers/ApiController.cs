using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Quiz7Mojos.Server.Controllers
{
    [Authorize]
    [Route("[controller]")]
    [ApiController]
    public class ApiController : ControllerBase
    {
    }
}
