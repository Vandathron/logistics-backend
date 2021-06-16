using System;
namespace logistics_BE.Domain
{
    public class LogisticsCenter
    {
        public int Id { get; set; }

        public int CityId { get; set; }

        public City City { get; set; }
    }
}
