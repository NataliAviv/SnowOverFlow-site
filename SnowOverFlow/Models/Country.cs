using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace SnowOverFlow.Models
{
    public class Country
    {
        [Key]
        public int ID { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Language { get; set; }

        [Required]
        public string Currency { get; set; }

        [Required]
        public int ContinentID { get; set; }

        [ForeignKey("ContinentID")]
        public virtual Continent Continent { get; set; }

        public Country() { }

    }

    //public enum continent { Asia,Europe,North_America,South_America,Australia }
}
