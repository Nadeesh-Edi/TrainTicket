/******************************************************************************
 * TrainTicketDatabaseSettings.cs
 * 
 * Description: This file contains the definition of the Database configurations class,
 * which provides the references to the data in appsettings.json
 * 
 * 
 *****************************************************************************/

namespace TrainTicketApi.Models
{
    public class TrainTicketDatabaseSettings
    {
        public string ConnectionString { get; set; } = null!;

        public string DatabaseName { get; set; } = null!;

        public string UserCollectionName { get; set; } = null!;

        public string TravellerCollectionName { get; set; } = null!;

        public string TrainCollectionName { get; set; } = null!;

        public string ScheduleCollectionName { get; set; } = null!;

        public string ReservationCollectionName { get; set; } = null!;
    }
}
