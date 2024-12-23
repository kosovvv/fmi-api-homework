using CarsAPI.Dtos;
using CarsAPI.Services;
using Microsoft.AspNetCore.Mvc;

namespace CarsAPI.Controllers
{
    public class CarsController(ICarService carService) : BaseApiController
    {
        [HttpGet("{id}")]
        public async Task<ActionResult<ResponseCarDTO>> GetById(long id)
        {
            ResponseCarDTO? carDTO = await carService.GetCarById(id);

            if (carDTO == null)
            {
                return NotFound();
            }

            return Ok(carDTO);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<ResponseCarDTO>> Update(long id, UpdateCarDTO dto)
        {
            ResponseCarDTO? carDTO = await carService.GetCarById(id);

            if (carDTO == null)
            {
                return NotFound();
            }

            ResponseCarDTO? result = await carService.UpdateCarById(id, dto);

            if (result == null)
            {
                return NotFound();
            }

            return Ok(result);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(long id)
        {
            ResponseCarDTO? carDTO = await carService.GetCarById(id);

            if (carDTO == null)
            {
                return NotFound();
            }

            await carService.DeleteCarById(id);

            return Ok();
        }

        [HttpGet]
        public async Task<ActionResult<ResponseCarDTO>> Get([FromQuery] string? carMake,
    [FromQuery] long? garageId,
    [FromQuery] int? startYear,
    [FromQuery] int? endYear)
        {
            IEnumerable<ResponseCarDTO> result = await carService.SearchCarsAsync(carMake, garageId, startYear, endYear);

            return Ok(result);
        }

        [HttpPost]
        public async Task<ActionResult<ResponseCarDTO>> Create(CreateCarDTO dto)
        {
            ResponseCarDTO? created = await carService.CreateCar(dto);

            if (created == null)
            {
                return NotFound();
            }

            return Ok(created);
        }

        private readonly ICarService carService = carService;
    }
}
