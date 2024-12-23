using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CarsAPI.Models
{
    public class Garage
    {
        public Garage()
        {
            this.Cars = [];
        }

        [Key]
        public long Id { get; set; }

        public required string Name { get; set; }

        public required string Location { get; set; }

        public required string City { get; set; }
        
        public int Capacity { get; set; }

        [InverseProperty(nameof(Car.Garages))]
        public virtual ICollection<Car> Cars { get; set; }

    }
}
