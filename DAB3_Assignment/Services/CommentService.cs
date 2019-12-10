using DAB3_Assignment.Models;
using MongoDB.Driver;
using System.Collections.Generic;
using System.Linq;

namespace DAB3_Assignment.Services
{
    public class CommentService
    {
        private readonly IMongoCollection<Comment> _comments;

        public CommentService(ISocialNetworkDatabaseSettings settings)
        {
            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);

            _comments = database.GetCollection<Comment>(settings.CommentsCollectionName);
        }

        public List<Comment> Get() =>
            _comments.Find(comment => true).ToList();

        public Comment Get(string id) =>
            _comments.Find<Comment>(comment => comment.ID == id).FirstOrDefault();

        public Comment Create(Comment comment)
        {
            _comments.InsertOne(comment);
            return comment;
        }

        public void Update(string id, Comment commentIn) =>
            _comments.ReplaceOne(comment => comment.ID == id, commentIn);

        public void Remove(Comment bookIn) =>
            _comments.DeleteOne(comment => comment.ID == bookIn.ID);

        public void Remove(string id) =>
            _comments.DeleteOne(comment => comment.ID == id);
    }
}