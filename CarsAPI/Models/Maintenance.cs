using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CarsAPI.Models
{
    public class Maintenance
    {
        [Key]
        public int Id { get; set; }

        [ForeignKey(nameof(Car))]
        public int CarId { get; set; }

        public required Car Car { get; set; }

        public required string ServiceType { get; set; }

        public DateTime ScheduledDate { get; set; }

        [ForeignKey(nameof(Garage))]
        public int GarageId { get; set; }

        public required Garage Garage { get; set; }
    }
}
