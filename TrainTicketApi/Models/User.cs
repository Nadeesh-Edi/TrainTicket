/******************************************************************************
 * User.cs
 * 
 * Description: This file contains the definition of the User model class,
 * which represents details of a user in the train ticket system.
 * The users includes Base User and Travel Agent
 * Role - 1 Base User
 * Role - 2 Travel Agent
 * 
 * 
 *****************************************************************************/

using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.Diagnostics;
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

        // Role - 1 Base User
        // Role - 2 Travel Agent
        public int Role { get; set; }

        public string Email { get; set; } = null!;

        public string Phone { get; set; } = null!;

        public string Pwrd { get; set; } = null!;

        public int Status { get; set; }
    }
}
