using Microsoft.AspNetCore.Mvc;

namespace CarsAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public abstract class BaseApiController : ControllerBase { }
}
