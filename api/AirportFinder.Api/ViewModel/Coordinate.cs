namespace AirportFinder.Api.ViewModel
{
    public class Coordinate
    {
        public string Name { get; set; }
        public string Latitude { get; set; }
        public string Longitude { get; set; }

        public Coordinate(string name, string latitude, string longitude)
        {
            this.Name = name;
            this.Latitude = latitude;
            this.Longitude = longitude;

        }
    }
}