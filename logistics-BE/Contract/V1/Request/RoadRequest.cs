using System;
namespace logistics_BE.Contract.V1.Request
{
    public class RoadRequest
    {
        public int StartCityid { get; set; }

        public int EndCityId { get; set; }

        public double Distance { get; set; }
    }
}
