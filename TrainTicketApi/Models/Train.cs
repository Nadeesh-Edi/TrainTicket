/******************************************************************************
 * Train.cs
 * 
 * Description: This file contains the definition of the Train model class,
 * which represents details of a train object.
 * 
 * 
 *****************************************************************************/

using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using System.Text.Json.Serialization;

namespace TrainTicketApi.Models
{
    [BsonIgnoreExtraElements]
    public class Train
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; }

        public string Name { get; set; } = null!;

        public string Model { get; set; } = null!;

        public string StartDestination { get; set; } = null!;

        public string EndDestination { get; set; } = null!;
    }
}
