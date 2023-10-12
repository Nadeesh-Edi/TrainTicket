/******************************************************************************
 * ReservationResponse.cs
 * 
 * Description: This file contains the definition of the ReservationResponse model class,
 * which represents the response details of a train ticket reservation.
 * 
 * 
 *****************************************************************************/

namespace TrainTicketApi.Models
{
    public class ReservationResponse
    {
        public string Id { get; set; }

        public string TrainName { get; set; }

        public DateOnly Date { get; set; }

        public ReservationResponse(string id, string trainName, DateOnly date, string startTime, int pax)
        {
            Id = id;
            TrainName = trainName;
            Date = date;
            StartTime = startTime;
            this.pax = pax;
        }

        public string StartTime { get; set; }

        public int pax { get; set; }
    }
}
