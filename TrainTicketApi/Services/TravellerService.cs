/******************************************************************************
 * TravellerService.cs
 * 
 * Description: This file contains the TravellerService class, which handles
 * database configurations and provides basic CRUD methods for the Traveller model.
 * 
 * 
 *****************************************************************************/

using TrainTicketApi.Models;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace TrainTicketApi.Services
{
    public class TravellerService
    {
        private readonly IMongoCollection<Traveller> _travellersCollection;

        public TravellerService(
        IOptions<TrainTicketDatabaseSettings> trainTicketDatabaseSettings)
        {
            var mongoClient = new MongoClient(
                trainTicketDatabaseSettings.Value.ConnectionString);

            var mongoDatabase = mongoClient.GetDatabase(
                trainTicketDatabaseSettings.Value.DatabaseName);

            _travellersCollection = mongoDatabase.GetCollection<Traveller>(
                trainTicketDatabaseSettings.Value.TravellerCollectionName);
        }

        // Get all Travellers as a List
        public async Task<List<Traveller>> GetAsync() =>
            await _travellersCollection.Find(_ => true).ToListAsync();

        // Get a Traveller by id
        public async Task<Traveller?> GetAsync(string id) =>
            await _travellersCollection.Find(x => x.Id == id).FirstOrDefaultAsync();

        // Save a new Traveller object to the db
        public async Task CreateAsync(Traveller newUser) =>
            await _travellersCollection.InsertOneAsync(newUser);

        // Update an existing Traveller given the id
        public async Task UpdateAsync(string id, Traveller updatedUser) =>
            await _travellersCollection.ReplaceOneAsync(x => x.Id == id, updatedUser);

        // Delete an existing Traveller given the id
        public async Task RemoveAsync(string id) =>
            await _travellersCollection.DeleteOneAsync(x => x.Id == id);
    }
}
