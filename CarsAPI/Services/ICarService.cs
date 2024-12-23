using CarsAPI.Dtos;

namespace CarsAPI.Services
{
    public interface ICarService
    {
        Task<ResponseCarDTO?> GetCarById(long id);
        Task DeleteCarById(long id);
        Task<ResponseCarDTO?> UpdateCarById(long id, UpdateCarDTO dto);
        Task<ResponseCarDTO?> CreateCar(CreateCarDTO dto);
        Task<IEnumerable<ResponseCarDTO>> SearchCarsAsync(string carMake, long? garageId, int? fromYear, int? toYear);
    }
}
