using LaikableDogsAPI.DataAccess.Interfaces;
using LaikableDogsAPI.Models.Requests;
using LaikableDogsAPI.Services.Interfaces;
using LaikalableDogsAPI.Models;

namespace LaikableDogsAPI.Services
{
    public class DogService : IDogService
    {
        private readonly IDogProvider dogProvider;

        public DogService(IDogProvider dogProvider)
        {
            this .dogProvider = dogProvider;
        }
        public async Task<Dog> GetDogById(Guid id)
        {
            return await dogProvider.GetDogById(id);
        }
        public async Task<IEnumerable<Dog>> GetAllDogs()
        {
            return await dogProvider.GetAllDogs();
        }

        public async Task<IEnumerable<Dog>> GetSortedDogs(SortingRequest request)
        {
            return await dogProvider.GetSortedDogs(request);
        }

        public async Task<IEnumerable<Dog>> GetDogFriends(string name)
        {
            return await dogProvider.GetDogFriends(name);
        }

        public async Task CreateDog(Dog request)
        {
            await dogProvider.CreateDog(request);
        }

        public async Task UpdateDog(Guid id, Dog request)
        {
            await dogProvider.UpdateDog(id, request);
        }

        public async Task DeleteDog(Guid id)
        {
            await dogProvider.DeleteDog(id);
        }
    }
}
