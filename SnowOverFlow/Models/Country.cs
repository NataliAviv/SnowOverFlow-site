using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SnowOverFlow.Models
{
    public class Country
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Language { get; set; }
        public string Currency { get; set; }
        public int continent { get; set; }
    }

    public enum continent {Asia,Europe,North_America,South_America,Australia }
}
