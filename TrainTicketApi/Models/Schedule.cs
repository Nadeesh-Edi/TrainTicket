/******************************************************************************
 * Schedule.cs
 * 
 * Description: This file contains the definition of the Schedule model class,
 * which represents details of train schedules and their availability.
 * 
 * 
 *****************************************************************************/

using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using System.Text.Json.Serialization;

namespace TrainTicketApi.Models
{
    [BsonIgnoreExtraElements]
    public class Schedule
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; }

        public string TrainName { get; set; } = null!;

        public string StartDestination { get; set; } = null!;

        public string EndDestination { get; set; } = null!;

        public List<Station> StationList { get; set; } = null!;

        public int Seats { get; set; }

        public DateOnly Date { get; set; }

        public string StartTime { get; set; }

        public int Status { get; set; }
    }
}
