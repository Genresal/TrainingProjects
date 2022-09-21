using LaikableDogsAPI.Models.Requests;
using LaikalableDogsAPI.Models;

namespace LaikableDogsAPI.Services.Interfaces
{
    public interface IDogService
    {
        Task<Dog> GetDogById(Guid id);
        Task<IEnumerable<Dog>> GetAllDogs();
        Task<IEnumerable<Dog>> GetSortedDogs(SortingRequest request);
        Task<IEnumerable<Dog>> GetDogFriends(string name);
        Task CreateDog(Dog request);
        Task UpdateDog(Guid id, Dog request);
        Task DeleteDog(Guid id);
    }
}
