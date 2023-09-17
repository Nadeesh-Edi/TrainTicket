namespace TrainTicketApi.Models
{
    public class TrainTicketDatabaseSettings
    {
        public string ConnectionString { get; set; } = null!;

        public string DatabaseName { get; set; } = null!;

        public string UserCollectionName { get; set; } = null!;

        public string TravellerCollectionName { get; set; } = null!;
    }
}
