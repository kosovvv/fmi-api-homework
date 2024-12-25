using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Cars.Data.Models.Models
{
    public class Car
    {
        public Car()
        {
            Garages = [];
        }

        [Key]
        public long Id { get; set; }

        public required string Make { get; set; }

        public required string Model { get; set; }

        public int ProductionYear { get; set; }

        public required string LicensePlate { get; set; }

        //[InverseProperty(nameof(Garage.Cars))]
        public virtual ICollection<Garage> Garages { get; set; }
    }
}
