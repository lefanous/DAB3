using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DAB3_Assignment.Models
{
    public class User
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string ID { get; set; }
        
        [BsonElement("name")]
        public string Name { get; set; }
        
        [BsonElement("gender")]
        public string Gender { get; set; }
        
        [BsonElement("age")]
        public int Age { get; set; }

        [BsonElement("followers")]
        public List<UserReference> Followers { get; set; }

        [BsonElement("blocked")]
        public List<UserReference> BlockedList { get; set; }

        public List<Update> Updates { get; set; }

    }
}
