using DAB3_Assignment.Models;
using DAB3_Assignment.Controllers;
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

        public Update Get(string id) =>
            _updates.Find<Update>(update => update.Author.ID == id).FirstOrDefault();

        public Update Create(Update update)
        {
            _updates.InsertOne(update);
            return update;
        }

        public async void NewComment(Comment comment)
        {
            var list = new List<Comment> { comment };

            var filter = Builders<Update>.Filter.Eq("ID", comment.UpdateID);
            var update = Builders<Update>.Update.Set("Comments", list);
            await _updates.UpdateOneAsync(filter, update);

        }
    }
}
