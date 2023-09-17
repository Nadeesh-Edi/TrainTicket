// Model for User object

using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.Text.Json.Serialization;

namespace TrainTicketApi.Models
{
    [BsonIgnoreExtraElements]
    public class User
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; }

        public string Name { get; set; } = null!;

        public int Role { get; set; }

        public string Email { get; set; } = null!;

        public string Phone { get; set; } = null!;

        public string Pwrd { get; set; } = null!;

        public int Status { get; set; }
    }
}
