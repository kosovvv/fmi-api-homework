using Cars.Data.Services.Interfaces;
using Cars.Web.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace CarsAPI.Controllers
{
    public class GaragesController(IGarageService garageService) : BaseApiController
    {
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<ResponseGarageDTO>> Get(long id)
        {
            ResponseGarageDTO? garageDTO = await garageService.Get(id);

            if (garageDTO == null)
            {
                return NotFound();
            }

            return Ok(garageDTO);
        }

        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<ResponseGarageDTO>> Update(long id, UpdateGarageDTO dto)
        {
            ResponseGarageDTO? result = await garageService.Update(id, dto);

            if (result == null)
            {
                return NotFound();
            }

            return Ok(result);
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult> Delete(long id)
        {
            await garageService.Delete(id);

            return Ok();
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<ResponseGarageDTO>> Search([FromQuery] string? city)
        {
            IEnumerable<ResponseGarageDTO> result = await garageService.Search(city);

            return Ok(result);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<ResponseGarageDTO>> Create(CreateGarageDTO dto)
        {
            ResponseGarageDTO? created = await garageService.Create(dto);

            return Ok(created);
        }

        [HttpGet("dailyAvailabilityReport")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<GarageDailyAvailabilityReportDTO>> Get(
        [FromQuery] long garageId,
        [FromQuery] DateTime? startDate,
        [FromQuery] DateTime? endDate)
        {
            IEnumerable<GarageDailyAvailabilityReportDTO> result = 
                await garageService.GetReport(garageId, startDate, endDate);

            return Ok(result);
        }

        private readonly IGarageService garageService = garageService;
    }
}
