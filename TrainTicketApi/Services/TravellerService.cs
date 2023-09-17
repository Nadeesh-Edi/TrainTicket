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

        public async Task<List<Traveller>> GetAsync() =>
            await _travellersCollection.Find(_ => true).ToListAsync();

        public async Task<Traveller?> GetAsync(string id) =>
            await _travellersCollection.Find(x => x.Id == id).FirstOrDefaultAsync();

        public async Task CreateAsync(Traveller newUser) =>
            await _travellersCollection.InsertOneAsync(newUser);

        public async Task UpdateAsync(string id, Traveller updatedUser) =>
            await _travellersCollection.ReplaceOneAsync(x => x.Id == id, updatedUser);

        public async Task RemoveAsync(string id) =>
            await _travellersCollection.DeleteOneAsync(x => x.Id == id);
    }
}
