using System.ComponentModel.DataAnnotations;
using AirportFinder.Api.Enums;

namespace AirportFinder.Api.ViewModel
{
    public class HomeViewModel
    {
        [Required]
        public string Address { get; set; }
        public int Distance { get; set; }
        public ETypeAirport TypeAirport { get; set; }
        
    }
}