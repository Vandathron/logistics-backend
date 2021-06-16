using System;
namespace logistics_BE.Contract.V1.Responses
{
    public class RoadResponse
    {
        public int StartCityId { get; set; }

        public int EndCityId { get; set; }

        public int Id { get; set; }

        public double Distance { get; set; }

        public string Name { get; set; }

    }
}
