using AirportFinder.Api.Models;
using MongoDB.Driver;

namespace AirportFinder.Api.Database
{
    public class DatabaseConnection
    {
        private IMongoClient client;
        private IMongoDatabase database;

        public DatabaseConnection()
        {
            this.client = new MongoClient("mongodb://mongo:1qaz2wsx@localhost:27017");            
            this.database = client.GetDatabase("geo");
        }

        public IMongoCollection<Airport> Airports => this.database.GetCollection<Airport>("airports");        
    }
}