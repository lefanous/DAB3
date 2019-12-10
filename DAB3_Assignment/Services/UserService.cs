using DAB3_Assignment.Models;
using DAB3_Assignment.Controllers;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DAB3_Assignment.Services
{
    public class UserService
    {
        private readonly IMongoCollection<User> _users;

        public UserService(ISocialNetworkDatabaseSettings settings)
        {
            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);

            _users = database.GetCollection<User>(settings.UsersCollectionName);
        }

        public List<User> Get() =>
            _users.Find(user => true).ToList();

        public User Get(string id) =>
            _users.Find<User>(user => user.ID == id).FirstOrDefault();

        public User Create(User user)
        {
            _users.InsertOne(user);
            return user;
        }

        public async void AddFollower(User _user, User _visitor)
        {         
            var filter = Builders<User>.Filter.Eq("ID", _user.ID);
            var visitorRef = new UserReference { ID = _visitor.ID, Name = _visitor.Name };
            var UpdatedFollowers = _user.Followers;
            UpdatedFollowers.Add(visitorRef);
            var update = Builders<User>.Update.Set("Followers", UpdatedFollowers);
            await _users.UpdateOneAsync(filter, update);
        }

        public async void Block(User _user, User _visitor)
        {
            var filter = Builders<User>.Filter.Eq("ID", _user.ID);
            var visitorRef = new UserReference { ID = _visitor.ID, Name = _visitor.Name };
            var UpdatedBlockedList = _user.BlockedList;
            UpdatedBlockedList.Add(visitorRef);
            var update = Builders<User>.Update.Set("BlockedList", UpdatedBlockedList);
            await _users.UpdateOneAsync(filter, update);
        }
        public async void NewUpdate(Update updates)
        {
            var list = new List<Update> { updates };

            var filter = Builders<User>.Filter.Eq("ID", updates.Author.ID);
            var update = Builders<User>.Update.Set("Updates", list);
            await _users.UpdateOneAsync(filter, update);

        }

        public void Update(string id, User userIn) =>
            _users.ReplaceOne(user => user.ID == id, userIn);

        public void Remove(User userIn) =>
            _users.DeleteOne(user => user.ID == userIn.ID);

        public void Remove(string id) =>
            _users.DeleteOne(user => user.ID == id);
    }
}
