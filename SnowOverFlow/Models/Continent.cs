using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace SnowOverFlow.Models
{
    public class Continent
    {
        [Key]
        public int ID { get; set; }

        [Required]
        public string Name { get; set; }

        
        public virtual ICollection<Country> Countries { get; set; }
    }
}
