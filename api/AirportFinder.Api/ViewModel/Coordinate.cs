namespace AirportFinder.Api.ViewModel
{
    public class Coordinate
    {
        public string Name { get; set; }
        public string Latitude { get; set; }
        public string Longitude { get; set; }

        public Coordinate(string name, string longitude, string latitude)
        {
            this.Name = name;
            this.Longitude = longitude;
            this.Latitude = latitude;

        }
    }
}