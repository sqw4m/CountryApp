using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CountryDataApplication.Models
{
    public class Country
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string NumericCode { get; set; }
        public int CityId { get; set; }
        public City City { get; set; }
        public double Area { get; set; }
        public int Population { get; set; }
        public Region Region { get; set; }
    }
}
