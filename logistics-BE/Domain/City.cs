using System;
using System.ComponentModel.DataAnnotations;

namespace logistics_BE.Domain
{
    public class City
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        public string Tag { get; set; }

    }
}
