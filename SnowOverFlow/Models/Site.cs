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

        [ForeignKey("CompanyId")]
        public virtual Country Country { get; set; }

        public int Rank { get; set; }

        [Required]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}", ApplyFormatInEditMode = true)]
        public DateTime SeasonStart { get; set; }

        [Required]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}", ApplyFormatInEditMode = true)]
        public DateTime SeasonEnd { get; set; }

        //לשאול אם הכוונה ב: צפון, דרום, מרכז
        public long Location { get; set; }

        [Required]
        public int Pistes { get; set; }

        [Required]
        public DifficultyType Difficulty { get; set; }

        public double BeerPrice { get; set; }


        public enum DifficultyType
        {
          easy,medium,hard
        }

    }
}
