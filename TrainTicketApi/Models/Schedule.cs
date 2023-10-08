// Model class for Schedule

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

        public DateTime Date { get; set; }

        public DateTime StartTime { get; set; }

        public int status { get; set; }
    }
}
