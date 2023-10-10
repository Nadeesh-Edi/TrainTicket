// Service class for Schedule Model.
// Handles the db configs and basic crud methods to the db.

using TrainTicketApi.Models;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace TrainTicketApi.Services
{
    public class ScheduleService
    {
        private readonly IMongoCollection<Schedule> _schedulesCollection;

        public ScheduleService(
        IOptions<TrainTicketDatabaseSettings> trainTicketDatabaseSettings)
        {
            var mongoClient = new MongoClient(
                trainTicketDatabaseSettings.Value.ConnectionString);

            var mongoDatabase = mongoClient.GetDatabase(
                trainTicketDatabaseSettings.Value.DatabaseName);

            _schedulesCollection = mongoDatabase.GetCollection<Schedule>(
                trainTicketDatabaseSettings.Value.ScheduleCollectionName);
        }

        // Get all Schedules as a List
        public async Task<List<Schedule>> GetAsync() =>
            await _schedulesCollection.Find(_ => true).ToListAsync();

        // Get all Active Schedules as a List
        public async Task<List<Schedule>> GetActiveAsync() =>
            await _schedulesCollection.Find(x => x.Status == 1).ToListAsync();

        // Get a Schedules by id
        public async Task<Schedule?> GetAsync(string id) =>
            await _schedulesCollection.Find(x => x.Id == id).FirstOrDefaultAsync();

        // Get a Schedules by Date
        public async Task<List<Schedule>> GetAsyncByDate(DateOnly date) =>
            await _schedulesCollection.Find(x => x.Date == date).ToListAsync();

        // Save a new Schedules object to the db
        public async Task CreateAsync(Schedule newSchedule) =>
            await _schedulesCollection.InsertOneAsync(newSchedule);

        // Update an existing Schedules given the id
        public async Task UpdateAsync(string id, Schedule updatedSchedule) =>
            await _schedulesCollection.ReplaceOneAsync(x => x.Id == id, updatedSchedule);

        // Delete an existing Schedules given the id
        public async Task RemoveAsync(string id) =>
            await _schedulesCollection.DeleteOneAsync(x => x.Id == id);
    }
}
