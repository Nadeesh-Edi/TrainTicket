/******************************************************************************
 * CurrentReservation.cs
 * 
 * Description: This file contains the definition of the CurrentReservation model class,
 * which represents the Response model sent for a reservation.
 * 
 * 
 *****************************************************************************/

namespace TrainTicketApi.Models
{
    public class CurrentReservation
    {
        public CurrentReservation(string id, string trainName, DateOnly date, string reservationStart, string reservationEnd, int pax, string scheduleId, string travellerId)
        {
            Id = id;
            TrainName = trainName;
            Date = date;
            ReservationStart = reservationStart;
            ReservationEnd = reservationEnd;
            Pax = pax;
            ScheduleId = scheduleId;
            this.travellerId = travellerId;
        }

        public string Id { get; set; }
        public string TrainName { get; set; }
        public DateOnly Date { get; set;}
        public string ReservationStart { get; set; }
        public string ReservationEnd { get; set;}
        public int Pax { get; set;}
        public string ScheduleId { get; set; }

        public string travellerId { get; set; }
    }
}
