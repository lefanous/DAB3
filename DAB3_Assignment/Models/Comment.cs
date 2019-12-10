using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DAB3_Assignment.Models
{
    public class Comment
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string ID { get; set; }

        [BsonElement("author")]
        public UserReference Author { get; set; }

        [BsonElement("content")]
        public string Content { get; set; }

        [BsonElement("creationtime")]
        public DateTime CreationTime { get; set; }

        [BsonElement("updateID")]
        public string UpdateID { get; set; }
    }
}
