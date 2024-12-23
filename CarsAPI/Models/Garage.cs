using System.ComponentModel.DataAnnotations;

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

        //[InverseProperty(nameof(Car.Garages))]
        public virtual ICollection<Car> Cars { get; set; }

        public virtual ICollection<Maintenance> Maintenances { get; set; }

    }
}
