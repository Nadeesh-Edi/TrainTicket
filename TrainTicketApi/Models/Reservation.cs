/******************************************************************************
 * Reservation.cs
 * 
 * Description: This file contains the definition of the Reservation model class,
 * which represents the details of a reservation for train tickets.
 * 
 * 
 *****************************************************************************/

using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using System.Text.Json.Serialization;

namespace TrainTicketApi.Models
{
    [BsonIgnoreExtraElements]
    public class Reservation
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; }

        public string TravellerId { get; set; } = null!;

        public string ScheduleId { get; set; } = null!;

        public string reservationStart{ get; set; } = null!;

        public string reservationEnd { get; set; } = null!;

        public int pax { get; set; }
    }
}
