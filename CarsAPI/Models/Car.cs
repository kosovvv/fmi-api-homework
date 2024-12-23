using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CarsAPI.Models
{
    public class Car
    {
        public Car()
        {
            this.Garages = [];
        }

        [Key]
        public int Id { get; set; }

        public required string Make { get; set; }

        public required string Model { get; set; }

        public int ProductionYear { get; set; }

        public required string LicensePlate { get; set; }

        [InverseProperty(nameof(Garages))]
        public virtual ICollection<Garage> Garages { get; set; }
    }
}
