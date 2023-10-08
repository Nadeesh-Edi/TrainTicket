namespace TrainTicketApi.Models
{
    public class ReservationResponse
    {
        public string Id { get; set; }

        public string TrainName { get; set; }

        public DateTime Date { get; set; }

        public ReservationResponse(string id, string trainName, DateTime date, DateTime startTime, int pax)
        {
            Id = id;
            TrainName = trainName;
            Date = date;
            StartTime = startTime;
            this.pax = pax;
        }

        public DateTime StartTime { get; set; }

        public int pax { get; set; }
    }
}
