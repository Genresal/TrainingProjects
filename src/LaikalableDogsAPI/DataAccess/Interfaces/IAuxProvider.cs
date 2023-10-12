using LaikalableDogsAPI.Models;

namespace LaikableDogsAPI.DataAccess.Interfaces
{
    public interface IAuxProvider
    {
        Task CreateDefaultData();
        Task DropCollections();
    }
}
