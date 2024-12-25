using System.ComponentModel.DataAnnotations;

namespace Cars.Data.Models.Models
{
    public class Garage
    {
        public Garage()
        {
            Cars = [];
            Maintenances = [];
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
