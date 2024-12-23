using System.ComponentModel.DataAnnotations;

namespace CarsAPI.Dtos
{
    public class GarageDailyAvailabilityReportDTO
    {
        [DataType(DataType.Date)]
        public string? Date { get; set; }

        public int Requests { get; set; }

        public int AvaliableCapacity { get; set; }
    }
}
