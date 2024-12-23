using System.ComponentModel.DataAnnotations;

namespace CarsAPI.Dtos
{
    public class GarageDailyAvailabilityReportDTO
    {
        public GarageDailyAvailabilityReportDTO()
        {
            
        }
        public GarageDailyAvailabilityReportDTO(string? date, int requests, int avaliableCapacity)
        {
            Date = date;
            Requests = requests;
            AvaliableCapacity = avaliableCapacity;
        }

        [DataType(DataType.Date)]
        public string? Date { get; set; }

        public int Requests { get; set; }

        public int AvaliableCapacity { get; set; }
    }
}
