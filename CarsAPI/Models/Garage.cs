using System.ComponentModel.DataAnnotations;

namespace CarsAPI.Models
{
    public class Garage
    {
        [Key]
        public int Id { get; set; }

        public required string Name { get; set; }

        public required string Location { get; set; }

        public required string City { get; set; }
        
        public int Capacity { get; set; }

    }
}
