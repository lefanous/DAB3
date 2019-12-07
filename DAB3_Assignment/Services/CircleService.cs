using DAB3_Assignment.Models;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DAB3_Assignment.Services
{
    public class CircleService
    {
        private readonly IMongoCollection<Circle> _circle;

        public CircleService(ISocialNetworkDatabaseSettings settings)
        {
            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);

            _circle = database.GetCollection<Circle>(settings.CirclesCollectionName);
        }

        public List<Circle> Get() =>
            _circle.Find(circle => true).ToList();

        public Circle Get(string id) =>
            _circle.Find<Circle>(circle => circle.ID == id).FirstOrDefault();

        public Circle Create(Circle circle)
        {
            _circle.InsertOne(circle);
            return circle;
        }

        public void Update(string id, Circle circleIn) =>
            _circle.ReplaceOne(circle => circle.ID == id, circleIn);

        public void Remove(Circle circleIn) =>
            _circle.DeleteOne(circle => circle.ID == circleIn.ID);

        public void Remove(string id) =>
            _circle.DeleteOne(circle => circle.ID == id);
    }
}
