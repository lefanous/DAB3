using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DAB3_Assignment.Models;
using DAB3_Assignment.Services;
using MongoDB.Driver;
using Microsoft.Extensions.Configuration;

namespace DAB3_Assignment.SeedData
{

public class Seeding
    {
        private readonly IMongoCollection<User> _user;
        private readonly IMongoCollection<Circle> _circle;

        public  Seeding(IConfiguration config)
        {
            
            var db = new MongoClient("mongodb://localhost:27017");
            var database = db.GetDatabase("SocialNetwork");
            _user = database.GetCollection<User>("Users");
            _circle = database.GetCollection<Circle>("Circles");
            
            Seed(_user, _circle);          
        }
        public List<User> GetUsers()
        {
            return _user.Find(user => true).ToList();
        }

        static async void Seed(IMongoCollection<User> users, IMongoCollection<Circle> circle)
        {
            
            var userlist = new List<User>
            {
                new User
                {
                    Name = "Sakariye Mahamed Ali",
                    Age = 22,
                    Gender = "Male",
                    Followers = new List<UserReference>(),
                    BlockedList = new List<UserReference>(),
                    Circles = new List<string>()
                },
                new User
                {
                    Name = "Abdallah Ajjawi",
                    Age = 21,
                    Gender = "Male",
                    Followers = new List<UserReference>(),
                    BlockedList = new List<UserReference>(),
                    Circles = new List<string>()
                },
                new User
                {
                    Name = "Zabih Mansoor",
                    Age = 22,
                    Gender = "Male",
                    Followers = new List<UserReference>(),
                    BlockedList = new List<UserReference>(),
                    Circles = new List<string>()
                }
            };
            var filter = Builders<User>.Filter.Empty;
            var result = users.DeleteMany(filter);
            await users.InsertManyAsync(userlist);

            var SakaFollowers = new List<UserReference> { new UserReference { ID = userlist[1].ID, Name = userlist[1].Name }, new UserReference { ID = userlist[2].ID, Name = userlist[2].Name }
        };

            var filter1 = Builders<User>.Filter.Eq("Name", "Sakariye Mahamed Ali");
            var update = Builders<User>.Update.Set("Followers", SakaFollowers);
            await users.UpdateOneAsync(filter1, update);

            var circlelist = new List<Circle>
            {
                new Circle
                {
                    Name = "Venner",
                    Users = new List<string>()

                },

                new Circle
                {
                    Name = "Familie",
                    Users = new List<string>()

                },

                new Circle
                {
                    Name = "Skole",
                    Users = new List<string>()

                }
            };

            var deletecircles = Builders<Circle>.Filter.Empty;
            var resultdlc = circle.DeleteMany(deletecircles);

            await circle.InsertManyAsync(circlelist);

            var circleven = new List<string> { userlist[0].Name, userlist[1].Name };

            var filter2 = Builders<Circle>.Filter.Eq("Name", "Venner");
            var update2 = Builders<Circle>.Update.Set("Users", circleven);
            await circle.UpdateOneAsync(filter2, update2);


        }
    }
}
