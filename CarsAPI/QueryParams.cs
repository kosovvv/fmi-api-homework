namespace CarsAPI
{
    public class QueryParams
    {
        public long? CarId 
        { 
            get => carId; 
            set => carId = value; 
        }
        public long? GarageId 
        { 
            get => garageId; 
            set => garageId = value; 
        }

        public string? StartDate 
        { 
            get => startDate; 
            set => startDate = value; 
        }
        public string? EndDate 
        { 
            get => endDate; 
            set => endDate = value; 
        }

        private long? carId;
        private long? garageId;
        private string? startDate;
        private string? endDate;
    }
}
