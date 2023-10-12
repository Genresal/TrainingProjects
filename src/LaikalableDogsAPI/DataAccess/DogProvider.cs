using LaikableDogsAPI.DataAccess.Interfaces;
using LaikableDogsAPI.Models;
using LaikalableDogsAPI.Models;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using LaikableDogsAPI.Models.Enums;
using LaikableDogsAPI.Models.Requests;
using MongoDB.Bson;
using System.Linq;
using System.Text.Json.Serialization;
using MongoDB.Bson.Serialization;

namespace LaikableDogsAPI.DataAccess
{
    public class DogProvider : IDogProvider
    {
        private readonly IMongoCollection<Dog> dogCollection;
        public DogProvider(IOptions<DatabaseSettings> mongoDBSettings)
        {
            MongoClient client = new MongoClient(mongoDBSettings.Value.ConnectionString);
            IMongoDatabase database = client.GetDatabase(mongoDBSettings.Value.DatabaseName);
            this.dogCollection = database.GetCollection<Dog>(mongoDBSettings.Value.DogsCollectionName);
        }

        public async Task<Dog> GetDogById(Guid id)
        {
            return await dogCollection.Find(x => x.Id == id).SingleOrDefaultAsync();
        }

        public async Task<DogParameters> GetDogParameters(Guid id)
        {
            var dogParamsProjection = Builders<Dog>.Projection.Expression(x => new DogParameters
            {
                Id = x.Id,
                Name = x.Name,
                Size = x.Size,
                Height = x.Height,
                Weight = x.Weight,
                Width = x.Width,
            });

            return await dogCollection.Find(x => x.Id == id).Project(dogParamsProjection).SingleOrDefaultAsync();
        }

        public async Task<IEnumerable<Dog>> GetAllDogs()
        {
            return await dogCollection.Find(_ => true).ToListAsync();
        }

        public async Task<IEnumerable<Dog>> GetSortedDogs(SortingRequest request)
        {
            var skipSize = request.PageSize * request.Page;
            var startId = (await dogCollection
                    .Find(new BsonDocument())
                    .Project(new BsonDocument { { "_id", 1 } })
                    .Sort(new BsonDocument("_id", (int)request.SortingDirection))
                    .ToListAsync())
                    .Select(x => x[0])
                    .Skip(skipSize)
                    .FirstOrDefault();

            var gteFilter = Builders<Dog>.Filter.Gte("Id", startId);
            var lteFilter = Builders<Dog>.Filter.Lte("Id", startId);
            return await dogCollection.Find(request.SortingDirection == SortingDirection.Ascending ? gteFilter : lteFilter)
                .Sort(new BsonDocument("_id", (int)request.SortingDirection))
                .Limit(request.PageSize).ToListAsync();
        }

        public async Task<IEnumerable<Dog>> GetDogFriends(string name)
        {
            var dog = await dogCollection.Find(x => x.Name == name)
                .FirstOrDefaultAsync();

            var filter = Builders<Dog>.Filter.AnyIn("_id", dog.DogFriends);
            return await dogCollection.Find(filter).ToListAsync();
        }

        public async Task AddFriend(Guid dogId, Guid friendId)
        {
            var filter = Builders<Dog>.Filter.Eq(x => x.Id, dogId);
            var dog = dogCollection.Find(filter).FirstOrDefault();

            if (dog.DogFriends == null)
            {
                await dogCollection.UpdateOneAsync(filter, Builders<Dog>.Update.Set(x => x.DogFriends, new List<Guid>()));
            }

            await dogCollection.UpdateOneAsync(filter, Builders<Dog>.Update.AddToSet(y => y.DogFriends, friendId));
        }
        public async Task CreateDog(Dog request)
        {
            await dogCollection.InsertOneAsync(request);
        }

        public async Task UpdateDog(Guid id, Dog request)
        {
            await dogCollection.ReplaceOneAsync(x => x.Id == id, request);
        }

        public async Task DeleteDog(Guid id)
        {
            await dogCollection.DeleteOneAsync(x => x.Id == id);
        }
    }
}
