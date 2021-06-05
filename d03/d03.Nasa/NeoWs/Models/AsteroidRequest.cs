using System;

namespace d03.Nasa.NeoWs.Models
{
    public class AsteroidRequest
    {
        public readonly DateTime StartDate;
        public readonly DateTime EndDate;
        public readonly int ResultCount;

        public AsteroidRequest(DateTime startDate, DateTime endDate, int count = int.MaxValue)
        {
            EndDate = endDate;
            StartDate = startDate;
            ResultCount = count;
        }
    }
}
