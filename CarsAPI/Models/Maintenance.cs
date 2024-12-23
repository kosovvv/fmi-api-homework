using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CarsAPI.Models
{
    public class Maintenance
    {
        [Key]
        public long Id { get; set; }

        [ForeignKey(nameof(Car))]
        public required long CarId { get; set; }

        public Car Car { get; set; }

        public required string ServiceType { get; set; }

        public DateTime ScheduledDate { get; set; }

        [ForeignKey(nameof(Garage))]
        public required long GarageId { get; set; }

        public Garage Garage { get; set; }
    }
}
