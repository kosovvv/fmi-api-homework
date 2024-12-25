using Cars.Data.Services.Interfaces;
using Cars.Web.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace CarsAPI.Controllers
{
    public class CarsController(ICarService carService) : BaseApiController
    {
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<ResponseCarDTO>> Get(long id)
        {
            ResponseCarDTO? carDTO = await carService.Get(id);

            if (carDTO == null)
            {
                return NotFound();
            }

            return Ok(carDTO);
        }

        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<ResponseCarDTO>> Update(long id, UpdateCarDTO dto)
        {
            ResponseCarDTO result = await carService.Update(id, dto);

            return Ok(result);
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult> Delete(long id)
        {
            await carService.Delete(id);

            return Ok();
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<ResponseCarDTO>> Get([FromQuery] string? carMake,
        [FromQuery] long? garageId,
        [FromQuery] int? startYear,
        [FromQuery] int? endYear)
        {
            IEnumerable<ResponseCarDTO> result = await carService.Search(carMake, garageId, startYear, endYear);

            return Ok(result);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<ResponseCarDTO>> Create(CreateCarDTO dto)
        {
            ResponseCarDTO created = await carService.Create(dto);

            return Ok(created);
        }

        private readonly ICarService carService = carService;
    }
}
