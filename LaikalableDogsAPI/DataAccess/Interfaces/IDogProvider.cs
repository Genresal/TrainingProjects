using LaikableDogsAPI.Models.Requests;
using LaikalableDogsAPI.Models;
using Microsoft.AspNetCore.Mvc;

namespace LaikableDogsAPI.DataAccess.Interfaces
{
    public interface IDogProvider
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
