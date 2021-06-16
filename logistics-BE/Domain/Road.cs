using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace logistics_BE.Domain
{
    public class Road
    {
        public int Id { get; set; }

        public int StartCityId { get; set; }

        public int EndCityId { get; set; }

        public double Distance { get; set; }

        [ForeignKey("StartCityId")]
        public virtual City StartCity { get; set; }

        [ForeignKey("EndCityId")]
        public virtual City EndCity { get; set; }

    }
}
