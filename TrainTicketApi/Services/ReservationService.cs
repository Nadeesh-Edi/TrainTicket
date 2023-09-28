using Microsoft.Extensions.Options;
using MongoDB.Driver;
using TrainTicketApi.Models;

namespace TrainTicketApi.Services
{
    public class ReservationService
    {
        private readonly IMongoCollection<Reservation> _reservationCollection;

        public ReservationService(
        IOptions<TrainTicketDatabaseSettings> trainTicketDatabaseSettings)
        {
            var mongoClient = new MongoClient(
                trainTicketDatabaseSettings.Value.ConnectionString);

            var mongoDatabase = mongoClient.GetDatabase(
                trainTicketDatabaseSettings.Value.DatabaseName);

            _reservationCollection = mongoDatabase.GetCollection<Reservation>(
                trainTicketDatabaseSettings.Value.ReservationCollectionName);
        }

        // Get all Schedules as a List
        public async Task<List<Reservation>> GetAsync() =>
            await _reservationCollection.Find(_ => true).ToListAsync();

        // Get a Schedules by id
        public async Task<Reservation?> GetAsync(string id) =>
            await _reservationCollection.Find(x => x.Id == id).FirstOrDefaultAsync();

        // Get a Schedules by user
        public async Task<List<Reservation>> GetUsersResAsync(string id) =>
            await _reservationCollection.Find(x => x.TravellerId == id).ToListAsync();

        // Save a new Schedules object to the db
        public async Task CreateAsync(Reservation newReservation) =>
            await _reservationCollection.InsertOneAsync(newReservation);

        // Update an existing Schedules given the id
        public async Task UpdateAsync(string id, Reservation updatedReservation) =>
            await _reservationCollection.ReplaceOneAsync(x => x.Id == id, updatedReservation);

        // Delete an existing Schedules given the id
        public async Task RemoveAsync(string id) =>
            await _reservationCollection.DeleteOneAsync(x => x.Id == id);
    }
}
