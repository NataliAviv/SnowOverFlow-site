using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SnowOverFlow.Models
{
    public class Site
    {
        [Key]
        public int ID { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public int CountryId { get; set; }

        [ForeignKey("CountryId")]
        public virtual Country Country { get; set; }

        [Range(1, 10)]
        public int Rank { get; set; }

        [Required]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}", ApplyFormatInEditMode = true)]
        public DateTime SeasonStart { get; set; }

        [Required]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}", ApplyFormatInEditMode = true)]
        public DateTime SeasonEnd { get; set; }

        [Required]
        public double Latitude { get; set; }

        [Required]
        public double Longtitude { get; set; }

        [Required]
        [Range(1,1000)]
        public int Pistes { get; set; }

        [Required]
        [Range(1,5)]
        public double Difficulty { get; set; }

        [Range(0,100)]
        public double BeerPrice { get; set; }

        
        //public enum DifficultyType { easy,medium,hard }

    }
}
