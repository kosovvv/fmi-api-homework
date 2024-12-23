namespace CarsAPI
{
    public static class DateUtils
    {
        public static List<MonthChunk> ChunkToMonths(DateTime startDate, DateTime endDate)
        {
            var chunks = new List<MonthChunk>();
            DateTime current = new(startDate.Year, startDate.Month, 1);

            while (current <= endDate)
            {
                var monthChunk = new MonthChunk
                {
                    Start = new DateTime(current.Year, current.Month, 1),
                    End = new DateTime(current.Year, current.Month, 1).AddMonths(1).AddDays(-1),
                    Month = current.ToString("MMMM").ToUpper(),
                    Year = current.Year,
                    MonthValue = current.Month - 1,
                    LeapYear = DateTime.IsLeapYear(current.Year)
                };

                chunks.Add(monthChunk);
                current = current.AddMonths(1);
            }

            return chunks;
        }

        public static List<DayChunk> ChunkToDays(DateTime startDate, DateTime endDate)
        {
            var chunks = new List<DayChunk>();
            DateTime current = startDate;

            while (current <= endDate)
            {
                chunks.Add(new DayChunk
                {
                    Start = current.Date,
                    End = current.Date.AddDays(1).AddMilliseconds(-1)
                });

                current = current.AddDays(1);
            }

            return chunks;
        }
    }

    public class MonthChunk
    {
        public DateTime Start { get; set; }
        public DateTime End { get; set; }
        public string Month { get; set; }
        public int Year { get; set; }
        public int MonthValue { get; set; }
        public bool LeapYear { get; set; }
    }

    public class DayChunk
    {
        public DateTime Start { get; set; }
        public DateTime End { get; set; }
    }
}