using MongoDB.Bson.Serialization.Attributes;

namespace DAB3_Assignment.Models
{
    public class UserReference
    {
        [BsonElement("_id")]
        public string ID { get; set; }

        [BsonElement("name")]
        public string Name { get; set; }
    }
}