using DAB3_Assignment.Models;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DAB3_Assignment.Services
{
    public class UpdateService
    {
        private readonly IMongoCollection<Update> _updates;

        public UpdateService(ISocialNetworkDatabaseSettings settings)
        {
            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);

            _updates = database.GetCollection<Update>(settings.UpdatesCollectionName);
        }

        public List<Update> Get() =>    
            _updates.Find(update => true).ToList();
    }
}
