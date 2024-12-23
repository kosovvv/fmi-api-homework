using CarsAPI.Dtos;
using CarsAPI.Services;
using Microsoft.AspNetCore.Mvc;

namespace CarsAPI.Controllers
{
    public class GaragesController(IGarageService garageService) : BaseApiController
    {
        [HttpGet("{id}")]
        public async Task<ActionResult<ResponseGarageDTO>> GetById(long id)
        {
            ResponseGarageDTO? garageDTO = await garageService.GetGarageById(id);

            if (garageDTO == null)
            {
                return NotFound();
            }

            return Ok(garageDTO);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<ResponseGarageDTO>> Update(long id, UpdateGarageDTO dto)
        {
            ResponseGarageDTO? garageDTO = await garageService.GetGarageById(id);

            if (garageDTO == null)
            {
                return NotFound();
            }

            ResponseGarageDTO? result = await garageService.UpdateGarageById(id, dto);

            if (result == null)
            {
                return NotFound();
            }

            return Ok(result);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(long id)
        {
            ResponseGarageDTO? garageDto = await garageService.GetGarageById(id);

            if (garageDto == null)
            {
                return NotFound();
            }

            await garageService.DeleteGarageById(id);

            return Ok();
        }

        [HttpGet]
        public async Task<ActionResult<ResponseGarageDTO>> GetByCity([FromQuery] string? city)
        {
            IEnumerable<ResponseGarageDTO> result = await garageService.GetGaragesByCity(city);

            return Ok(result);
        }

        [HttpPost]
        public async Task<ActionResult<ResponseGarageDTO>> Create(CreateGarageDTO dto)
        {
            ResponseGarageDTO? created = await garageService.CreateGarage(dto);

            if (created == null)
            {
                return NotFound();
            }

            return Ok(created);
        }

        [HttpGet("dailyAvailabilityReport")]
        public async Task<ActionResult<GarageDailyAvailabilityReportDTO>> Get([FromQuery] long? garageId,
        [FromQuery] DateTime? startDate,
        [FromQuery] DateTime? endDate)
        {
            IEnumerable<GarageDailyAvailabilityReportDTO> result =
                await garageService.GetDailyAvailabilityReport(garageId.Value, startDate.Value, endDate.Value);

            return Ok(result);
        }

        private readonly IGarageService garageService = garageService;
    }
}
