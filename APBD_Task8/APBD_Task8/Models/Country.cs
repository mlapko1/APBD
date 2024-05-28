using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using APBD_Task8.Models;

namespace APBD_Task8.Models
{
    public class Country
    {
        [Key]
        public int IdCountry { get; set; }
        [Required]
        public string Name { get; set; }

        public ICollection<CountryTrip> CountryTrips { get; set; }
    }
}