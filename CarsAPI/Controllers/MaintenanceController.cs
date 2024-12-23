using Microsoft.AspNetCore.Mvc;

namespace CarsAPI.Controllers
{
    public class MaintenanceController : BaseApiController
    {
        [HttpGet]
        public async Task<ActionResult<string>> Test()
        {
            return await Task.FromResult("test");
        }
    }
}
