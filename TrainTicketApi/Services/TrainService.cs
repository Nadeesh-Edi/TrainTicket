// Service class for Train Model.
// Handles the db configs and basic crud methods to the db.

using TrainTicketApi.Models;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace TrainTicketApi.Services
{
    public class TrainService
    {
        private readonly IMongoCollection<Train> _trainsCollection;

        public TrainService(
        IOptions<TrainTicketDatabaseSettings> trainTicketDatabaseSettings)
        {
            var mongoClient = new MongoClient(
                trainTicketDatabaseSettings.Value.ConnectionString);

            var mongoDatabase = mongoClient.GetDatabase(
                trainTicketDatabaseSettings.Value.DatabaseName);

            _trainsCollection = mongoDatabase.GetCollection<Train>(
                trainTicketDatabaseSettings.Value.TrainCollectionName);
        }

        // Get all Trains as a List
        public async Task<List<Train>> GetAsync() =>
            await _trainsCollection.Find(_ => true).ToListAsync();

        // Get a Train by id
        public async Task<Train?> GetAsync(string id) =>
            await _trainsCollection.Find(x => x.Id == id).FirstOrDefaultAsync();

        // Save a new Train object to the db
        public async Task CreateAsync(Train newTrain) =>
            await _trainsCollection.InsertOneAsync(newTrain);

        // Update an existing Train given the id
        public async Task UpdateAsync(string id, Train updatedTrain) =>
            await _trainsCollection.ReplaceOneAsync(x => x.Id == id, updatedTrain);

        // Delete an existing Train given the id
        public async Task RemoveAsync(string id) =>
            await _trainsCollection.DeleteOneAsync(x => x.Id == id);
    }
}
