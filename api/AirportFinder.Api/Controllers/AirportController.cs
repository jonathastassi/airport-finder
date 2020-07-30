using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using AirportFinder.Api.Database;
using AirportFinder.Api.Enums;
using AirportFinder.Api.Models;
using AirportFinder.Api.ViewModel;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;
using MongoDB.Driver.GeoJsonObjectModel;
using Newtonsoft.Json;

namespace AirportFinder.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AirportController : ControllerBase
    {
        private readonly DatabaseConnection connection;
        public AirportController(DatabaseConnection connection)
        {
            this.connection = connection;
        }

        [HttpPost()]
        public async Task<IActionResult> Find([FromBody] HomeViewModel homeViewModel)
        {
            Coordinate coordinate = FindLocation(homeViewModel.Address);

            var airportNearest = new List<Coordinate>();
            airportNearest.Add(coordinate);

            double lon = Convert.ToDouble(coordinate.Longitude);
            double lat = Convert.ToDouble(coordinate.Latitude);

            string typeAirport = string.Empty;

            if (homeViewModel.TypeAirport == ETypeAirport.international) 
            {
                typeAirport = "international";
            }
            else if (homeViewModel.TypeAirport == ETypeAirport.Municipal) 
            {
                typeAirport = "municipal";
            }

            double distance = homeViewModel.Distance * 1000;

            var localization = new GeoJsonPoint<GeoJson2DGeographicCoordinates>(new GeoJson2DGeographicCoordinates(lon, lat));

            var constructor = Builders<Airport>.Filter;
            FilterDefinition<Airport> filter;

            if (typeAirport == string.Empty) 
            {
                filter = constructor.NearSphere<GeoJson2DGeographicCoordinates>(x => x.loc, localization, distance);
            }
            else 
            {
                filter = constructor.NearSphere<GeoJson2DGeographicCoordinates>(x => x.loc, localization, distance) & constructor.Eq(x => x.type, typeAirport);
            }

            var listAirport = await this.connection.Airports.Find(filter).ToListAsync();

            foreach (var airport in listAirport)
            {
                airportNearest.Add(new Coordinate(airport.name, airport.loc.Coordinates.Longitude.ToString(), airport.loc.Coordinates.Latitude.ToString()));
            }

            return Ok(airportNearest);
        }

        private Coordinate FindLocation(string address)
        {
            string url = $"https://maps.google.com/maps/api/geocode/json?address=${address}&key=AIzaSyC7O6r72_j2KQ6EyExg32GMIHDgALcUS8A";

            var coord = new Coordinate("Não localizado", "-10", "-10");
            var response = new WebClient().DownloadString(url);
            var googleGeocode = JsonConvert.DeserializeObject<GoogleGeocodeResponse>(response);

            if (googleGeocode.status == "OK") 
            {
                coord.Name = googleGeocode.results[0].formatted_address;
                coord.Longitude = googleGeocode.results[0].geometry.location.lng;
                coord.Latitude = googleGeocode.results[0].geometry.location.lat;
            }

            return coord;
        }
    }
}