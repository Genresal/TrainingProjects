using LaikableDogsAPI.DataAccess.Interfaces;
using LaikableDogsAPI.Models;
using LaikalableDogsAPI.Models;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using LaikableDogsAPI.Models.Enums;

namespace LaikableDogsAPI.DataAccess
{
    public class AuxProvider : IAuxProvider
    {
        private readonly IMongoDatabase database;
        private readonly IMongoCollection<Dog> dogCollection;
        public AuxProvider(IOptions<DatabaseSettings> mongoDBSettings)
        {
            MongoClient client = new MongoClient(mongoDBSettings.Value.ConnectionString);
            this.database = client.GetDatabase(mongoDBSettings.Value.DatabaseName);
            this.dogCollection = database.GetCollection<Dog>(mongoDBSettings.Value.DogsCollectionName);
        }

        public async Task CreateDefaultData()
        {
            for (int i = 0; i < 20; i++)
            {
                await dogCollection.InsertOneAsync(new Dog
                {
                    Name = $"name{i}",
                    Breed = Breed.Beagle,
                    Color = i,
                    Height = i,
                    Size = i,
                    Weight = i,
                    Width = i
                });
            }
        }

        public async Task DropCollections()
        {
            await database.DropCollectionAsync(nameof(Dog));
        }
    }
}
