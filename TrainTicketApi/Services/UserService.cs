/******************************************************************************
 * UserService.cs
 * 
 * Description: This file contains the UserService class, which handles
 * database configurations and provides basic CRUD methods for the User model.
 * 
 * 
 *****************************************************************************/

using TrainTicketApi.Models;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace TrainTicketApi.Services
{
    public class UserService
    {
        private readonly IMongoCollection<User> _usersCollection;

        public UserService(
        IOptions<TrainTicketDatabaseSettings> trainTicketDatabaseSettings)
        {
            var mongoClient = new MongoClient(
                trainTicketDatabaseSettings.Value.ConnectionString);

            var mongoDatabase = mongoClient.GetDatabase(
                trainTicketDatabaseSettings.Value.DatabaseName);

            _usersCollection = mongoDatabase.GetCollection<User>(
                trainTicketDatabaseSettings.Value.UserCollectionName);
        }

        // Get all Users as a List
        public async Task<List<User>> GetAsync() =>
            await _usersCollection.Find(_ => true).ToListAsync();

        // Get a User by id
        public async Task<User?> GetAsync(string id) =>
            await _usersCollection.Find(x => x.Id == id).FirstOrDefaultAsync();

        // Save a new User object to the db
        public async Task CreateAsync(User newUser) =>
            await _usersCollection.InsertOneAsync(newUser);

        // Update an existing User given the id
        public async Task UpdateAsync(string id, User updatedUser) =>
            await _usersCollection.ReplaceOneAsync(x => x.Id == id, updatedUser);

        // Delete an existing User given the id
        public async Task RemoveAsync(string id) =>
            await _usersCollection.DeleteOneAsync(x => x.Id == id);

        // Get a User by Email
        public async Task<List<User>> GetAsyncByEmail(string email) =>
            await _usersCollection.Find(x => x.Email == email).ToListAsync();
    }
}
