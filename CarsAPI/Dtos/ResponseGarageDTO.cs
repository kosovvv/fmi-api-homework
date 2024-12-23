using System.ComponentModel.DataAnnotations;

namespace CarsAPI.Dtos
{
    public class ResponseGarageDTO(long id, string name, string location, string city, int capacity)
    {
        [Required]
        public long Id { get; set; } = id;

        [Required]
        public string Name { get; set; } = name;

        [Required]
        public string Location { get; set; } = location;

        [Required]
        public string City { get; set; } = city;

        [Required]
        public int Capacity { get; set; } = capacity;
    }
}
