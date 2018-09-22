using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace SnowOverFlow.Models
{
    public class Site
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public int CountryId { get; set; }
        public int Rank { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}", ApplyFormatInEditMode = true)]
        public DateTime SeasonStart { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}", ApplyFormatInEditMode = true)]
        public DateTime SeasonEnd { get; set; }
        public long Location { get; set; }
        public int Pistes { get; set; }
        public DifficultyType Difficulty { get; set; }
        public int BeerPrice { get; set; }


        public enum DifficultyType
        {
        easy,medium,hard
        }

    }
}
