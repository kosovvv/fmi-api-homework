using CarsAPI.Dtos;
using CarsAPI.Services;
using Microsoft.AspNetCore.Mvc;

namespace CarsAPI.Controllers
{
    public class MaintenanceController(IMaintenanceService maintenanceService) : BaseApiController
    {
        [HttpGet("{id}")]
        public async Task<ActionResult<ResponseMaintenanceDTO>> GetById(long id)
        {
            ResponseMaintenanceDTO? maintenanceDTO = await maintenanceService.GetMaintenanceById(id);

            if (maintenanceDTO == null)
            {
                return NotFound();
            }

            return Ok(maintenanceDTO);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<ResponseMaintenanceDTO>> Update(long id, UpdateMaintenanceDTO dto)
        {
            ResponseMaintenanceDTO? maintenanceDTO = await maintenanceService.GetMaintenanceById(id);

            if (maintenanceDTO == null)
            {
                return NotFound();
            }

            ResponseMaintenanceDTO? result = await maintenanceService.UpdateMaintenanceById(id, dto);

            if (result == null)
            {
                return NotFound();
            }

            return Ok(result);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(long id)
        {
            ResponseMaintenanceDTO? maintenanceDTO = await maintenanceService.GetMaintenanceById(id);

            if (maintenanceDTO == null)
            {
                return NotFound();
            }

            await maintenanceService.DeleteMaintenanceById(id);

            return Ok();
        }

        [HttpGet]
        public async Task<ActionResult<ResponseMaintenanceDTO>> GetMaintenance([FromQuery] long? carId, 
            [FromQuery] long? garageId, 
            [FromQuery] DateTime? startDate, 
            [FromQuery] DateTime? endDate)
        {
            IEnumerable<ResponseMaintenanceDTO> result = await maintenanceService.
                GetMaintenanceByQueryParams(startDate.ToString(), endDate.ToString(), carId, garageId);

            return Ok(result);
        }

        [HttpPost]
        public async Task<ActionResult<ResponseMaintenanceDTO>> CreateMaintenance(CreateMaintenanceDTO dto)
        {
            ResponseMaintenanceDTO? created = await maintenanceService.CreateMaintenance(dto);

            if (created == null)
            {
                return NotFound();
            }

            return Ok(created);
        }

        [HttpGet("monthlyRequestsReport")]
        public async Task<ActionResult<ResponseMaintenanceDTO>> GetReport([FromQuery] long garageId,
            [FromQuery] DateTime startDate,
            [FromQuery] DateTime endDate)
        {
            IEnumerable<MonthlyRequestsReportDTO> result = await maintenanceService.
                GetMonthlyRequestsReportAsync(garageId, startDate, endDate);

            return Ok(result);
        }

        private readonly IMaintenanceService maintenanceService = maintenanceService;
    }
}
