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
                },
                new User
                {
                    Name = "Ali Kais",
                    Age = 21,
                    Gender = "Male",
                    Followers = new List<UserReference>(),
                    BlockedList = new List<UserReference>(),
                    Circles = new List<string>()
                },
                new User
                {
                    Name = "Jose Aldo",
                    Age = 33,
                    Gender = "Male",
                    Followers = new List<UserReference>(),
                    BlockedList = new List<UserReference>(),
                    Circles = new List<string>()
                },
                new User
                {
                    Name = "Daniel Day Lewis",
                    Age = 62,
                    Gender = "Male",
                    Followers = new List<UserReference>(),
                    BlockedList = new List<UserReference>(),
                    Circles = new List<string>()
                },
                new User
                {
                    Name = "Chantel Jeffries",
                    Age = 27,
                    Gender = "Female",
                    Followers = new List<UserReference>(),
                    BlockedList = new List<UserReference>(),
                    Circles = new List<string>()
                },
                new User
                {
                    Name = "Emily Ratajkowski",
                    Age = 28,
                    Gender = "Female",
                    Followers = new List<UserReference>(),
                    BlockedList = new List<UserReference>(),
                    Circles = new List<string>()
                }

            };
            var deleteusers = Builders<User>.Filter.Empty;
            var resultdlu = users.DeleteMany(deleteusers);

            await users.InsertManyAsync(userlist);

            var SakaFollowers = new List<UserReference>
            {
                new UserReference
                {
                    ID = userlist[1].ID, Name = userlist[1].Name
                },
                new UserReference
                {
                    ID = userlist[2].ID, Name = userlist[2].Name
                },
                new UserReference
                {
                    ID = userlist[3].ID, Name = userlist[3].Name
                },
                new UserReference
                {
                    ID = userlist[4].ID, Name = userlist[4].Name
                },
                new UserReference
                {
                    ID = userlist[5].ID, Name = userlist[5].Name
                },
                new UserReference
                {
                    ID = userlist[6].ID, Name = userlist[6].Name
                },
                new UserReference
                {
                    ID = userlist[7].ID, Name = userlist[7].Name
                }

            };
            var sakafilter = Builders<User>.Filter.Eq("ID", userlist[0].ID);
            var updatesf = Builders<User>.Update.Set("Followers", SakaFollowers);
            await users.UpdateOneAsync(sakafilter, updatesf);

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

            var circlevenner = new List<string> { "Venner" };

            var sakacircle = Builders<User>.Filter.Eq("ID", userlist[1].ID);
            var updatesc = Builders<User>.Update.Set("Circles", circlevenner);
            await users.UpdateOneAsync(sakacircle, updatesc);

            var abdacircle = Builders<User>.Filter.Eq("ID", userlist[0].ID);
            var updateac = Builders<User>.Update.Set("Circles", circlevenner);
            await users.UpdateOneAsync(sakacircle, updatesc);
        }
    }
}
