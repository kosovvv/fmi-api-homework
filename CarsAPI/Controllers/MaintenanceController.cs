using Cars.Data.Services.Interfaces;
using Cars.Web.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace CarsAPI.Controllers
{
    public class MaintenanceController(IMaintenanceService maintenanceService) : BaseApiController
    {
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<ResponseMaintenanceDTO>> Get(long id)
        {
            ResponseMaintenanceDTO? maintenanceDTO = await maintenanceService.Get(id);

            if (maintenanceDTO == null)
            {
                return NotFound();
            }

            return Ok(maintenanceDTO);
        }

        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<ResponseMaintenanceDTO>> Update(long id, UpdateMaintenanceDTO dto)
        {
            ResponseMaintenanceDTO? result = await maintenanceService.Update(id, dto);

            return Ok(result);
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult> Delete(long id)
        {
            await maintenanceService.Delete(id);

            return Ok();
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<ResponseMaintenanceDTO>> Get([FromQuery] long? carId, 
            [FromQuery] long? garageId, 
            [FromQuery] DateTime? startDate, 
            [FromQuery] DateTime? endDate)
        {
            IEnumerable<ResponseMaintenanceDTO> result = await maintenanceService.
                Search(startDate.ToString(), endDate.ToString(), carId, garageId);

            return Ok(result);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<ResponseMaintenanceDTO>> Create(CreateMaintenanceDTO dto)
        {
            ResponseMaintenanceDTO? created = await maintenanceService.Create(dto);

            if (created == null)
            {
                return NotFound();
            }

            return Ok(created);
        }

        [HttpGet("monthlyRequestsReport")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<ResponseMaintenanceDTO>> GetReport([FromQuery] long garageId,
            [FromQuery] DateTime? startMonth,
            [FromQuery] DateTime? endMonth)
        {
            IEnumerable<MonthlyRequestsReportDTO> result = await maintenanceService.GetReport(garageId, startMonth, endMonth);

            return Ok(result);
        }

        private readonly IMaintenanceService maintenanceService = maintenanceService;
    }
}
