using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace SnowOverFlow.Models
{
    public class SnowContext : DbContext
    {
        public SnowContext() 
            : base()
        {
        }
        public System.Data.Entity.DbSet<SnowOverFlow.Models.Continent> Continents { get; set; }


        public System.Data.Entity.DbSet<SnowOverFlow.Models.Country> Countries { get; set; }

        public System.Data.Entity.DbSet<SnowOverFlow.Models.Site> Sites { get; set; }

    }
}
