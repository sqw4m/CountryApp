using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace CountryDataApplication.Models
{
    public class CountriesDbContext : DbContext
    {
        public DbSet<Region> Regions { get; set; }
        public DbSet<City> Cities { get; set; }
        public DbSet<Country> Countries { get; set; }

        public CountriesDbContext(DbContextOptions<CountriesDbContext> options)
            : base(options)
        {
        }
    }
}
