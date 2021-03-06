﻿using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DAB3_Assignment.Models
{
    public class Update
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string ID { get; set; }

        [BsonElement("author")]
        public UserReference Author { get; set; }

        [BsonElement("posttype")]
        public string PostType { get; set; }

        [BsonElement("url")]
        public string Url { get; set; }

        [BsonElement("content")]
        public string Content { get; set; }

        [BsonElement("creationtime")]
        public DateTime CreationTime { get; set; }

        [BsonElement("comments")]
        public List<Comment> Comments { get; set; }

        [BsonElement("circles")]
        public string Circles { get; set; }
    }
}
